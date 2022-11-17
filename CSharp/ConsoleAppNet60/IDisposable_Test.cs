using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet60
{
    /// <summary>
    /// IDisposible接口哲学
    /// 参考：https://zhuanlan.zhihu.com/p/244894004
    /// </summary>
    internal class IDisposable_Test
    {
        /// <summary>
        /// endpoint
        /// </summary>
        public static void Run()
        {
            /*
             * 实现了IDisposable接口的类型，C#提供了using关键字自动执行Dispose方法，这种方式是显式调用的
             * 执行完using语句，会输出显示调用的提示
             * using语法块编译后，会生成类似如下结构，也就是说using是一个语法糖：
             * var myclass = new MyDisposibleClass();
             * try
             * {
             * 
             * }
             * finally
             * {
             *      myclass.Dispose();
             * }
             * **/
            using (var myclass = new MyDisposibleClass())
            {
                //do some task
            }

            /*
             * 由GC调度终结器释放，对象生存周期会提升一代，并且在第一次回收时，调用终结器
             * **/
            GCFinalize();

            //这里显式触发GC，正常情况下，不需要手动执行
            GC.Collect(0);

            //GC终结器调用的时机由GC决定，因此，大多数情况，下面这行输出会在终结器日志前展示
            Console.WriteLine("All Done");
        }

        /// <summary>
        /// 这里单独提供一个方法是因为局部变量会在方法结束时切断GC ROOT路径，如果放在同一个方法内测试，GC不会将该变量识别为不可达（即便将变量赋值为null也是不行的）
        /// </summary>
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static void GCFinalize()
        {
            var mycalss = new MyDisposibleClass();
        }

        /// <summary>
        /// 如果对象的某些字段或属性是IDisposable的子类，比如FileStream，那么这个类也应该实现IDisposable。
        /// 非普通类型包含普通的自身和非托管资源。那么，如果类的某个字段或属性的类型是非普通类型，那么这个类型也应该是非普通类型，应该也要实现IDisposable接口。
        /// </summary>
        class MyDisposibleClass : IDisposable
        {
            /// <summary>
            /// 模拟一个非托管资源
            /// 非托管资源：不受CLR控制的资源，也就是不属于.NET本身的功能，往往是通过调用跨平台程序集(如C++)或者操作系统提供的一些接口，比如Windows内核对象、文件操作、数据库连接、socket、Win32API、网络等。
            /// </summary>
            private IntPtr NativeResource { get; set; } = Marshal.AllocHGlobal(100);
            /// <summary>
            /// 模拟一个托管资源
            /// 托管资源：由CLR管理分配和释放的资源，也就是我们直接new出来的对象
            /// </summary>
            public Random? ManagedResource { get; set; } = new Random();
            /// <summary>
            /// 释放标记
            /// </summary>
            private bool disposed;

            /// <summary>
            /// 手动调用释放资源的Dispose
            /// </summary>
            public void Dispose()
            {
                Console.WriteLine("Explicit call 'Dispose' method");

                Dispose(true);
                /*
                 * 如果显式调用资源释放，因为手动释放过了，所以告诉GC不需要将对象提升生存期并且调用终结器了，减少内存开销
                 * **/
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// NOTE: Leave out the finalizer altogether if this class doesn't
            /// own unmanaged resources itself, but leave the other methods
            /// exactly as they are.
            /// 这个析构方法更规范的说法叫做终结器，它的意义在于，如果我们忘记了显式调用Dispose方法，垃圾回收器在扫描内存的时候，会作为释放资源的一种补救措施。
            /// 为什么加了析构方法就会有这种效果，我们知道在new对象的时候，CLR会为对象创建一块内存空间，一旦对象不再被引用，就会被垃圾回收器回收掉，对于没有实现IDisposable接口的类来说，垃圾回收时将直接回收掉这片内存空间，而对于实现了IDisposable接口的类来说，由于析构方法的存在，在创建对象之初，CLR会将该对象的一个指针放到终结器列表中，在GC回收内存之前，会首先将终结器列表中的指针放到一个freachable队列中，同时，CLR还会分配专门的内存空间来读取freachable队列，并调用对象的终结器，只有在这个时候，对象才被真正的被标识为垃圾，在下一次垃圾回收的时候才回收这个对象所占用的内存空间。
            /// 那么，实现了IDisposable接口的对象在回收时要经过两次GC才能被真正的释放掉，因为GC要先安排CLR调用终结器，基于这个特点，如果我们显式调用了Dispose方法，那么GC就不会再进行第二次垃圾回收了，当然，如果忘记了Dispose，也避免了忘记调用Dispose方法造成的内存泄漏。
            /// 提示：析构方法是在C++中的一种说法，因为终结器和析构方法两者特点很像，为了沿袭C++的叫法，称之为析构方法也没有什么不妥，但它们又不完全一致，所以微软后来又确定它叫终结器。
            /// </summary>
            ~MyDisposibleClass()
            {
                Console.WriteLine("GC call finalizer");
                // Finalizer calls Dispose(false)
                Dispose(false);
            }

            /// <summary>
            /// The bulk of the clean-up code is implemented in Dispose(bool)
            /// 如果不是虚方法，那么就很有可能让开发者在子类继承的时候忽略掉父类的清理工作，所以，基于继承体系的原因，我们要提供这样的一个虚方法。
            /// 其次，提供的这个虚方法是一个带bool参数的，带这个参数的目的，是为了释放资源时区分对待托管资源和非托管资源，而实现自IDisposable的Dispose方法调用时，传入的是true，而终结器调用的时候，传入的是false，当传入true时代表要同时处理托管资源和非托管资源；而传入false则只需要处理非托管资源即可。
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                Console.WriteLine($"disposing value is {disposing}");
                /*
                 * 我们的Dispose模式设计思路在于：
                 * 如果显式调用Dispose，那么类型就该按部就班的将自己的资源全部释放，
                 * 如果忘记了调用Dispose，那就假定自己的所有资源(哪怕是非普通类型)都交给GC了，所以不需要手动清理。
                 * 所以这就理解为什么实现自IDisposable的Dispose中调用虚方法是传true，终结器中传false了。
                 * 
                 * 虚方法首先判断了disposed字段，这个字段用于判断对象的释放状态，这意味着多次调用Dispose时，如果对象已经被清理过了，那么清理工作就不用再继续。
                 * 
                 * Dispose并不代表把对象置为了null，且已经被回收彻底不存在了。但事实上，对象的引用还可能存在的，只是不再是正常的状态了，所以我们明白有时候我们调用数据库上下文有时候为什么会报“数据库连接已被释放”之类的异常了。
                 * 所以，disposed字段的存在，用来表示对象是否被释放过。
                 * **/
                if (disposed)
                    return;

                /* 
                 * 清理托管资源
                 * 
                 * 为什么要区别对待托管资源和非托管资源？
                 * 在这个问题之前，其实我们应该先弄明白：托管资源需要手动清理吗？
                 * 不妨将C#的类型分为两类：一类实现了IDisposable，另一类则没有。
                 * 前者我们定义为非普通类型，后者为普通类型。
                 * 非普通类型包含了非托管资源，实现了IDisposable，但又包含有自身是托管资源，所以不普通。
                 * 对于我们刚才的问题，答案就是：普通类型不需要手动清理，而非普通类型需要手动清理。
                 * **/
                if (disposing)
                {
                    if (ManagedResource != null)
                    {
                        ManagedResource = null;
                    }
                }

                //清理非托管资源
                if (NativeResource != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(NativeResource);
                    NativeResource = IntPtr.Zero;
                }

                //告诉自己已经被释放
                disposed = true;
            }
        }
    }
}
