using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CSharp.System_Threading.SynchronousConstruction.Primitive.KernelMode
{
    /// <summary>
    /// EventWaitHandle 使用信号 signaled 和 nonsignaled 来控制是否放行阻塞线程执行的
    /// ManualReset 完全依赖信号判断决定是否可以继续执行，因此会将一批等待的线程释放
    /// AutoReset 在标记信号为 signaled 后释放一个线程执行后会自动重置成 nonsignaled，因此可以用来限制单线程执行
    /// </summary>
    internal class EventWaitHandle_Text
    {
        /// <summary>
        /// entry point
        /// </summary>
        public static void Run()
        {
            ManualResetEvent_Test();
        }

        /// <summary>
        /// 手动重置事件
        /// 相当于子类：ManualResetEvent
        /// </summary>
        static void ManualResetEvent_Test()
        {
            //as same as ManualResetEvent class
            // Summary:
            //     When signaled, the System.Threading.EventWaitHandle releases all waiting threads
            //     and remains signaled until it is manually reset.
            var manualResetEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            SynchronizationTestHelper.ThreadStatusMonitorLog(t1);

            //每秒将信号设置成 signaled
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                //ManualReset，Set设置信号为 signaled 所有阻塞的线程将会释放
                manualResetEvent.Set();

                //重置信号为 nonsignaled，下次调用WaitOne的线程将会被阻塞
                manualResetEvent.Reset();
            }

            void DoTask()
            {
                for (int i = 0; i < 10; i++)
                {
                    //WaitOne 在 nonsignaled 下会阻塞线程，等待信号变为 signaled 后继续执行
                    var wait = manualResetEvent.WaitOne();

                    Console.WriteLine($"\n[{i}] current thread name is {Thread.CurrentThread.Name} {wait}");
                }
            }
        }

        /// <summary>
        /// 自动重置事件
        /// 相当于子类：AutoResetEvent
        /// </summary>
        static void AutoResetEvent_Test()
        {
            //as same as AutoResetEvent class
            // Summary:
            //     When signaled, the System.Threading.EventWaitHandle resets automatically after
            //     releasing a single thread. If no threads are waiting, the System.Threading.EventWaitHandle
            //     remains signaled until a thread blocks, and resets after releasing the thread.
            var autoResetEvent = new EventWaitHandle(false, EventResetMode.AutoReset);

            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            SynchronizationTestHelper.ThreadStatusMonitorLog(t1);

            //每秒将信号设置成 signaled
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                //AutoReset模式下，Set设置 == signaled ，释放一个线程后会自动关闭信号（设置成 nonsignaled），因此每次设置信号只能有一个线程执行
                autoResetEvent.Set();
            }

            void DoTask()
            {
                for (int i = 0; i < 10; i++)
                {
                    //WaitOne 在 nonsignaled 下会阻塞线程，等待信号变为 signaled 后继续执行
                    var re = autoResetEvent.WaitOne();

                    Console.WriteLine($"\n[{i}] current thread name is {Thread.CurrentThread.Name} {re}");
                }
            }
        }
    }
}
