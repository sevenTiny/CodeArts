﻿# 混合线程同步构造

“基元线程同步构造”讨论了基元用户模式和内核模式线程同步构造。其他所有线程同步构造都基于它们而构建，而且一般都合并了用户模式和内核模式构造，我们称为混合线程同步构造。没有线程竞争时，混合构造提供了基元用户模式构造所具有的性能优势。多个线程竞争一个构造时，混合构造通过基元内核模式的构造来提供不“自旋”的优势(避免浪费 CPU 时间)。

由于大多数应用程序的线程都很少同时竞争一个构造，所以性能上的增强可以使你的应用程序表现得更出色。

FCL 自带了许多混合构造，它们通过一些别致的逻辑将你的线程保持在用户模式，从而增强应用程序的性能。有的混合构造直到首次有线程在一个构造上发生竞争时，才会创建内核模式的构造。如果线程一直不在构造上发生竞争，应用程序就可避免因为创建对象而产生的性能损失，同时避免为对象分配内存。许多构造还支持使用一个 CancellationToken(参见第 27 章“计算限制的异步操作”)，使一个线程强迫解除可能正在构造上等待的其他线程的阻塞。

- ManualResetEventSlim 类
- SemaphoreSlim 类
- Monitor类
- 同步块
- ReaderWriterLockSlim 类
- CountdownEvent 类
- Barrier 类

## Monitor类
Monitor 根本就不该实现成静态类，它应该像其他所有同步构造那样实现。也就是说，应该是一个可以实例化并在上面调用实例方法的类。事实上，正因为 Monitor 被设计成一个静态类，所以它还存在其他许多问题。下面对这些额外的问题进行了总结。
- 变量能引用一个代理对象一一前提是变量引用的那个对象的类型派生自 Sstem.MarshalByRefObject 类(参见第22 章“CLR 寄宿和AppDomain”)。调用 Monitor的方法时，传递对代理对象的引用，锁定的是代理对象而不是代理引用的实际对象。
- 如果线程调用 Monitor.Enter，向它传递对类型对象的引用，而且这个类型对象是以“AppDomain 中立”的方式加的，线程就会跨越进程中的所有 AppDomain 在那个类型上获取锁。这是 CLR 一个已知的 bug，它破坏了 AppDomain 本应提供的隔离能力。这个 bug 很难在保证高性能的前提下修复，所以它一直没有修复。我的建议是，水远都不要向Monitor 的方法传递类型对象引用。
- 由于字符串可以留用(参见 14.2.2 节“字符是不可变的”)，所以两个完全独立的代码段可能在不知情的情况下获取对内存中的一个 String 对象的引用。如果将这个String 对象引用传给 Monitor 的方法，两个独立的代码段现在就会在不知情的情况下以同步方式执行”。
- 跨越 AppDomain 边界传递字符串时，CLR 不创建字符串的副本:相反，它只是将对字符串的一个引用传给其他 AppDomain。这增强了性能，理论上也是可行的，因为String 对象本来就不可变(不可修改)。但和其他所有对象一样，String 对象关联了一个同步块索引，这个索引是可变的(可修改)，使不同 AppDomain 中的线程在不知情的情况下开始同步。这是 CLR 的AppDomain 隔离存在的另一个 bug。我的建议是永远不要将 String引用传给 Monitor 的方法。
- 由于 Monitor 的方法要获取一个 Object，所以传递值类型会导致值类型被装箱，造成线程在已装箱对象上获取锁。每次调用 Monitor.Enter 都会在一个完全不同的对象上获取锁，造成完全无法实现线程同步。
- 向方法应用IMethodImpl(MethodImplptionsSynchronized)I特性，会造成JIT编译器用 Monitor.Enter 和 Monitor,Exit 调用包围方法的本机代码。如果方法是实例方法会将 this 传给 Monitor 的这些方法，锁定隐式公共的锁。如果方法是静态的，对类型的类型对象的引用会传给这些方法，造成锁定“AppDomain 中立”的类型。我的建议是永远不要使用这个特性。
- 调用类型的类型构造器时(参见8.3 节“类构造器”，CLR 要获取类型对象上的一个锁，确保只有一个线程初始化类型对象及其静态字段。同样地，这个类型可能以“AppDomain 中立”的方式加载，所以会出问题。例如，假定类型构造器的代码进入死循环，进程中的所行 AppDomain 都无法使用该类型。我的建议是尽量避免使用类型构造器，或者至少保持它们的短小和简单。

## ReaderWriterLockSlim 类
我们经常都希望让一个线程简单地读取一些数据的内容。如果这些数据被一个互斥锁(比如SimpleSpinLock，SimpleWaitLock，SimpleHybridLock，AnotherHybridLock，Mutex 或者 Monitor)保护，那么当多个线程同时试图访问这些数据时，只有一个线程才会运行，其他所有线程都会阻塞。这会造成应用程序伸缩性和吞吐能力的急剧下降。如果所有线程都希望以只读方式访问数据，就根本没有必要阻塞它们:应该允许它们并发地访问数据。另一方面，如果一个线程希望修改数据，这个线程就需要对数据的独占式访问。ReaderWriterLockSlim 构造封装了解决这个问题的逻辑。具体地说，这个构造像下面这样控制线程。

- 一个线程向数据写入时，请求访问的其他所有线程都被阻寨。
- 一个线程从数据读取时，请求读取的其他线程允许继续执行，但请求写入的线程仍被阻塞。
- 向线程写入的线程结束后，要么解除一个写入线程(writer)的阻塞，使它能向数据写入，要么解除所有读取线程(reader)的阻塞，使它们能并发读取数据。如果没有线程被阻塞，锁就进入可以自由使用的状态，可供下一个 reader 或 writer 线程获取。
- 从数据读取的所有线程结束后，一个 writer 线程被解除阻塞，使它能向数据写入。如果没有线程被阻塞，锁就进入可以自由使用的状态，可供下一个 reader 或 writer 线程获取。

这个构造有几个概念要特别留意。首先，ReaderWriterLockSlim 的构造器允许传递一个LockRecurionsPolicy 标志，它的定义如下:
public enum LockRecursionPolicy { NoRecursion， SupportsRecursion ]

如果传递 SupportsRecursion 标志，锁就支持线程所有权和递归行为。如同本章早些时候讨论的那样，这些行为对锁的性能有负面影响。所以，建议总是向构造器传递LockRecursionPolicy.NoRecursion(就像本例)。reader-writer 锁支持线程所有权和递归的代价非常高昂，因为锁必须跟踪曾允许进入锁的所有 reader 线程，同时为每个线程都单独维护递归计数。事实上，为了以线程安全的方式维护所有这些信息，ReaderWriterLockSlim内部要使用一个互斥的“自旋锁”， 我不是在开玩笑!

ReaderWriterLockSlim 类提供了一些额外的方法(前面没有列出)允许一个 reader 线程升级为 writer 线程。以后，线程可以把自己降级回 reader 线程。设计者的思路是，一个线程刚开始的时候可能是读取数据。然后，根据数据的内容，线程可能想对数据进行修改。为此,线程要把它自己从 reader 升级为 writer。锁如果支持这个行为，性能会大打折扣。而且我完全不觉得这是一个有用的功能。线程并不是直接从 reader 变成 writer 的。当时可能还有其他线程正在读取，这些线程必须完全退出锁。在此之后，尝试升级的线程才允许成为 writer。这相当于先让 reader 线程退出锁，再立即获取这个锁以进行写入。

> 注意 FCL还提供了一个ReaderWriterLock 构造,它是在MicrosoftNETFramework 1.0中引入的。这个构造存在许多问题，所以Microsoft 在NETFramework 3.s中引入了ReaderWriterLockSlim 构造。团队没有对原先的ReaderWriterLock 构造进行改进,因为他们害怕失去和那些正在使用它的应用程序的兼容性。下面列举了 ReaderWriterLock 存在的几个问题。首先，即使不存在线程竞争，它的速度也非常慢。其次，线程所有权和递归行为是这个构造强加的，完全取消不了，这使锁变得更慢。最后，相比 writer 线程，它更青睐于 reader 线程，所以 writer 线程可能排起好长的队，却很少有机会获得服务，最终造成“拒绝服务”(DOS)问题。

## CountdownEvent 类
这个构造阻塞一个线程，直到它的内部计数器变成 0。从某种角度说，这个构造的行为和 Semaphore 的行为相反。(Semaphore 是在计数为0 时阻寒线程。)

## Barrier 类
System.Threading.Barrier 构造用于解决一个非常稀有的问题，平时一般用不上。Barrier控制的一系列线程需要并行工作，从而在一个算法的不同阶段推进。或许通过一个例子更容易理解:当CLR 使用它的垃圾回收器(GC)的服务器版本时，GC 算法为每个内核都创建一个线程。这些线程在不同应用程序线程的栈中向上移动，并发标记堆中的对象。每个线程完成了它自己的那一部分工作之后，必须停下来等待其他线程完成。所有线程都标记好对象后，线程就可以并发地压缩(compact)堆的不同部分。每个线程都完成了对它的那一部分的堆的压缩之后，线程必须阻塞以等待其他线程。所有线程都完成了对自己那一部分的堆的压缩之后，所有线程都要在应用程序的线程的栈中上行，对根进行修正，使之引用因为压缩而发生了移动的对象的新位置。只有在所有线程都完成这个工作之后，垃圾回收器的工作才算真正完成，应用程序的线程现在可以恢复执行了。

构造 Barrier 时要告诉它有多少个线程准备参与工作，还可传递一个Action<Barrier>委托来引用所有参与者完成一个阶段的工作后要调用的代码。可以调用 AddParticipant 和RemoveParticipant 方法在 Barrier 中动态添加和删除参与线程。但在实际应用中，人们很少这样做。每个线程完成它的阶段性工作后，应调用 SignalAndWait,，告诉 Barrier 线程已经完成一个阶段的工作，而 Barrier 会阻寒线程(使用一个 ManualResetEventSlim)。所有参与者都调用了 SignalAndWait 后，Barrier 将调用指定的委托(由最后一个调用SignalAndWait 的线程调用)，然后解除正在等待的所有线程的阻塞，使它们开始下一阶段。