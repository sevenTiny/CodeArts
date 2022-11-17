using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet60.System_Threading
{
    /// <summary>
    /// https://learn.microsoft.com/zh-cn/dotnet/api/system.threading.monitor.enter?view=net-6.0
    /// </summary>
    internal class Monitor_Test
    {
        /// <summary>
        /// entry point
        /// </summary>
        public static void Run()
        {
            Monitor_Wait_Timeout();
        }

        private static object locker = new object();

        private static int waitCount = 0;

        /// <summary>
        /// monitor 'Monitor' locked status by 'isLocked' field
        /// </summary>
        /// <param name="threads"></param>
        static void LockStatusMonitor(params Thread[] threads)
        {
            //monitor lock status
            new Thread(() =>
            {
                while (threads.Any(t => t.ThreadState != ThreadState.Stopped))
                {
                    //1s per .
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        /// <summary>
        /// write thread message info second
        /// </summary>
        static void NoMonitor()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"\n[{i}] current thread name is {Thread.CurrentThread.Name}");

                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// write thread info per second
        /// </summary>
        static void SampleMonitor()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                for (int i = 0; i < 10; i++)
                {
                    Monitor.Enter(locker);

                    Console.WriteLine($"\n[{i}] current thread name is {Thread.CurrentThread.Name}");

                    Thread.Sleep(1000);

                    Monitor.Exit(locker);
                }
            }
        }

        /// <summary>
        /// wait and pulse
        /// </summary>
        static void Monitor_Wait_Pulse()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                Monitor.Enter(locker);

                Console.WriteLine($"\ncurrent thread name is {Thread.CurrentThread.Name}");

                Thread.Sleep(3000);

                //原子性判断是否调用过wait，如果已经调用过wait了，则是线程2，执行释放锁命令，让线程1得以重新获得锁继续执行
                if (Interlocked.CompareExchange(ref waitCount, 1, 0) == 0)
                {
                    /*
                     * Wait
                     * 释放对象上的锁并阻塞当前线程，直到它重新获取该锁，调用Wait后，线程进入等待队列 
                     **/
                    var status = Monitor.Wait(locker);

                    Console.WriteLine($"\nRe-GetLock current thread name is {Thread.CurrentThread.Name}, status is {status}");
                }
                //如果已经有执行wait（阻塞）的线程，那么其他线程执行释放
                else
                {
                    /*
                     * Pulse
                     * 通知等待队列中的线程锁定对象状态的更改。(等待队列 -> 就绪队列）
                     * 调用Pulse并不会释放锁，因此要调用Exit释放锁才会让上述阻塞队列进入就绪队列，并重新获得锁得以继续执行
                     **/
                    Monitor.Pulse(locker);
                    Monitor.Exit(locker);
                }
            }
        }

        /// <summary>
        /// wait timeout
        /// </summary>
        static void Monitor_Wait_Timeout()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                Monitor.Enter(locker);

                Console.WriteLine($"\ncurrent thread name is {Thread.CurrentThread.Name}");

                Thread.Sleep(3000);

                //原子性判断是否调用过wait，如果已经调用过wait了，则是线程2，执行释放锁命令，让线程1得以重新获得锁继续执行
                if (Interlocked.CompareExchange(ref waitCount, 1, 0) == 0)
                {
                    /*
                     * Wait
                     * 释放对象上的锁并阻塞当前线程，直到它重新获取该锁，调用Wait后，线程进入等待队列 
                     * 如果传Timeout参数，达到超时时间间隔，则线程进入就绪队列。
                     * 
                     * 关于返回值 status
                     * 如果传递了超时，达到超时时间前其他线程调用Pulse改变状态并释放锁后，返回true
                     * 超时后，返回false
                     **/
                    var status = Monitor.Wait(locker, 1000);

                    Console.WriteLine($"\nRe-GetLock current thread name is {Thread.CurrentThread.Name}, status is {status}");
                }
                //如果已经有执行wait（阻塞）的线程，那么其他线程执行释放
                else
                {
                    /*
                     * 这里不需要调用Pulse，因为Wait提供了超时自动改变状态的能力，1s后，线程自动从等待队列进入就绪队列
                     * 调用Exit释放锁，就绪队列的线程会自动继续执行
                     **/
                    //Monitor.Pulse(locker);
                    Monitor.Exit(locker);
                }
            }
        }

        /// <summary>
        /// wait timeout
        /// </summary>
        static void Monitor_Wait_TimeoutAndReturnTrue()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                Monitor.Enter(locker);

                Console.WriteLine($"\ncurrent thread name is {Thread.CurrentThread.Name}");

                Thread.Sleep(3000);

                //原子性判断是否调用过wait，如果已经调用过wait了，则是线程2，执行释放锁命令，让线程1得以重新获得锁继续执行
                if (Interlocked.CompareExchange(ref waitCount, 1, 0) == 0)
                {
                    /*
                     * Wait
                     * 释放对象上的锁并阻塞当前线程，直到它重新获取该锁，调用Wait后，线程进入等待队列 
                     * 如果传Timeout参数，达到超时时间间隔，则线程进入就绪队列。
                     * 
                     * 关于返回值 status
                     * 如果传递了超时，达到超时时间前其他线程调用Pulse改变状态并释放锁后，返回true
                     * 超时后，返回false
                     * 
                     * 注：这里设置5s超时时间，大于线程2释放锁时间，因此会返回true
                     **/
                    var status = Monitor.Wait(locker, 5000);

                    Console.WriteLine($"\nRe-GetLock current thread name is {Thread.CurrentThread.Name}, status is {status}");
                }
                //如果已经有执行wait（阻塞）的线程，那么其他线程执行释放
                else
                {
                    /*
                     * 这里调用了Pulse改变线程1就绪状态，下面逻辑会在线程1超时结束前执行
                     * 这种情况下，Wait返回的status会为true
                     **/
                    Monitor.Pulse(locker);
                    Monitor.Exit(locker);
                }
            }
        }
    }
}
