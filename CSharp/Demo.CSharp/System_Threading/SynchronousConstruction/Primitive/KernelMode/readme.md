# 内核模式构造

Windows 提供了几个内核模式的构造来同步线程，内核模式的构造比用户模式的构造慢得多，一个原因是它们要求 Widows 操作系统自身的配合，另一个原因是在内核对象上调用的每个方法都造成调用线程从托管代码转换为本机(native)用户模式代码，再转换为本机(native)内核模式代码。然后，还要朝相反的方向一路返回。这些转换需要大量 CPU 时间:经常执行会对应用程序的总体性能造成负面影响。

但内核模式的构造具备基元用户模式构造所不具备的优点:
- 内核模式的构造检测到在一个资源上的竞争时，Windows 会阻塞输掉的线程，使它不占着一个 CPU“自旋”(spinning)，无谓地浪费处理器资源。
- 内核模式的构造可实现本机(native)和托管(managed)线程相互之间的同步。
- 内核模式的构造可同步在同一台机器的不同进程中运行的线程。
- 内核模式的构造可应用安全性设置，防止未经授权的帐户访问它们。
- 线程可一直阻寒，直到集合中的所有内核模式构造都可用，或直到集合中的任何内核模式构造可用。
- 在内核模式的构造上阻塞的线程可指定超时值;指定时间内访问不到希望的资源，线程就可以解除阻寒并执行其他任务。

## WaitHandle
System,Threading命名空间提供了一个名为 WaitHandle 抽象基类。WaitHandle类是一个很简单的类，它唯一的作用就是包装一个 Windows 内核对象句柄。FCL 提供了几个从WaitHandle派生的类。所有类都在 System.Threading 命名空间中定义。

WaitHandle 基类内部有一个SafeWaitHandle 字段，它容纳了一个 Win32内核对象句柄。这个字段是在构造一个具体的 WaitHandle 派生类时初始化的。除此之外，WaitHandle类公开了由所有派生类继承的方法。在一个内核模式的构造上调用的每个方法都代表一个完整的内存栅栏。

这些方法有几点需要注意
- 可以调用WaitHandle 的WaitOne 方法让调用线程等待底层内核对象收到信号。这个方法在内部调用 Win32 WaitForSingleObjectEx函数。如果对象收到信号，返回的Boolean 是true;超时就返回false。
- 可以调用 WaitHandle的静态 WaitAll 方法，让调用线程等待 WaitHandlell中指定的所有内核对象都收到信号。如果所有对象都收到信号，返回的 Boolean 是 true;超时则返回 false。这个方法在内部调用 Win32 WaitForMultipleObjectsEx函数，为bWaitAll参数传递TRUE。
- 可以调用WaitHandle的静态WaitAny方法让调用线程等待WaitHandlell中指定的任何内核对象收到信号。返回的 Int32 是与收到信号的内核对象对应的数组元素索引;如果在等待期间没有对象收到信号，则返回WaitHandleWaitTimeout。这个方法在内部调用 Win32WaitForMultipleObjectsEx函数，为bWaitAll 参数传递FALSE。在传给 WaitAny 和 WaitAll 方法的数组中，包含的元素数不能超过64个，否则方法会抛出一个System.NotSupportedException
- 可以调用 WaitHandle的 Dispose 方法来关闭底层内核对象句柄这个方法在内部调用Win32 CloseHandle 函数。只有确定没有别的线程要使用内核对象才能显式调用Dispose。你需要写代码并进行测试，这是一个巨大的负担。所以我强烈反对显式调用Dispose: 相反，让垃圾回收器(GC)去完成清理工作。GC 知道什么时候没有线程使用对象，会自动进行清理。从某个角度看，GC 是在帮你进行线程同步!

不接受超时参数的那些版本的 WaitOne和 WaitAll 方法应返回oid 而不是 Boolean。原因是隐含的超时时间是无限长(System.Threading.Timeout.Infinite)，所以它们只会返回true。
调用任何这些方法时都不需要检查返回值。

EventWaitHandle，AutoResetEvent，ManualResetEvent，Semaphore 和 Mutex 类都派生自WaitHandle，因此它们继承了 WaitHandle 的方法和行为。

首先，所有这些类的构造器都在内部调用 Win32 CreateEvent(为 bManualReset 参数传递FALSE或TRUE)、CreateSemaphore 或 CreateMutex 函数。从所有这些调用返回的句柄值都保存在WaitHandle基类内部定义的一个私有SafeWaitHandle字段中。

其次，EventWaitHandle，Semaphore和 Mutex类都提供了静态 OpenExisting 方法，它们在内部调用 Win320penEvent，OpenSemaphore或OpenMutex函数，并传递一个String实参(标识现有的一个具名内核对象)。所有函数返回的句柄值都保存到从OpenExisting方法返回的一个新构造的对象中。如果指定名称的内核对象不存在，就抛出一个WaitHandleCannotBeOpenedException 异常

内核模式构造的一个常见用途是创建在任何时刻只允许它的一个实例运行的应用程序。这种单实例应用程序的例子包括 Microsoft Ofice Outlook,Windows Live Messenger，WindowsMedia Player和 Windows Media Center。

## Event 构造
事件(event)其实只是由内核维护的 Boolean 变量。事件为 false，在事件上等待的线程就阻寨:事件为 true，就解除阻塞。有两种事件，即自动重置事件和手动重置事件。当一个自动重置事件为 true 时，它只唤醒一个阻塞的线程，因为在解除第一个线程的阻塞后，内核将事件白动重置回 false，造成其余线程继续阻塞。而当一个手动重置事件为 true 时，它解除正在等待它的所有线程的阻塞，因为内核不将事件自动重置回 false: 现在，你的代码必须将事件手动重置回false。

- EventWaitHandle
- AutoResetEvent
- ManualResetEvent

## Semaphore 构造
信号量(semaphore)其实就是由内核维护的 Int32 变量。信号量为0时，在信号量上等待的线程会阻塞;信号量大于0时解除阻塞。在信号量上等待的线程解除阻寒时，内核自动从信号量的计数中减1。信号量还关联了一个最大 Int32 值当前计数绝不允许超过最大计数。

下面总结一下这三种内核模式基元的行为。
- 多个线程在一个自动重置事件上等待时，设置事件只导致一个线程被解除阻塞
- 多个线程在一个手动重置事件上等待时，设置事件导致所有线程被解除阻塞。
- 多个线程在一个信号量上等待时，释放信号量导致 releaseCount 个线程被解除阻塞(releaseCount 是传给 Semaphore的 Release 方法的实参)。

因此，自动重置事件在行为上和最大计数为 1 的信号量非常相似。两者的区别在于，可以在一个自动重置事件上连续多次调用 Set，同时仍然只有一个线程解除阻塞。相反，在一个信号量上连续多次调用 Release,会使它的内部计数一直递增,这可能解除大量线程的阻寒。顺便说一句，如果在一个信号量上多次调用 Release，会导致它的计数超过最大计数，这时Release 会抛出一个SemaphoreFullException。

## Mutex 构造

互斥体(mutex)代表一个互斥的锁。它的工作方式和AutoResetEvent(或者计数为1的Semaphore 相似)，三者都是一次只释放一个正在等待的线程。

互斥体有一些额外的逻辑，这造成它们比其他构造更复杂。首先，Mutex 对象会查询调用线程的Int32ID，记录是哪个线程获得了它。一个线程调用 ReleaseMutex 时，Mutex确保调用线程就是获取 Mutex 的那个线程。如若不然，Mutex 对象的状态就不会改变，而ReleaseMutex会抛出一个System,ApplicationException。另外，拥有Mutex的线程因为任何原因而终止，在 Mutex 上等待的某个线会因为抛出 System.Threading.AbandonedMutexException 异常而被唤醒。该异常通常会成为未处理的异常，从而终止整个进程。这是好事，因为线程在获取了一个 Mutex 之后，可能在更新完 Mutex 所保护的数据之前终止。如果其他线程捕捉了 AbandonedMutexException，就可能试图访问损坏的数据，造成无法预料的结果和安全隐患。

其次，Mutex 对象维护着一个递归计数(recursion count)，指出拥有该 Mutex 的线程拥有了它多少次。如果一个线程当前拥有一个 Mutex，而后该线程再次在 Mutex 上等待，计数就会递增，这个线程允许继续运行。线程调用 ReleaseMutex 将导致计数递减。只有计数变成0，另一个线程才能成为该 Mutex 的所有者。

大多数人都不喜欢这个额外的逻辑。这些“功能”是有代价的。Mutex 对象需要更多的内存来容纳额外的线程ID 和计数信息。更要紧的是，Mutex 代码必须维护这些信息，使锁变得更慢。如果应用程序需要(或希望)这些额外的功能，那么应用程序的代码可以自己实现:代码不一定要放到 Mutex 对象中。因此，许多人都会避免使用 Mutex 对象。