using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CSharp.System_Threading.SynchronousConstruction
{
    /// <summary>
    /// 同步测试帮助类
    /// </summary>
    internal abstract class SynchronizationTestHelper
    {
        /// <summary>
        /// monitor 'Monitor' locked status
        /// log '.' per second in independent thread
        /// display the others thread execute interval
        /// 其他线程执行的过程中，用独立线程每秒打点一次，用来直观展示其他线程的执行间隔
        /// </summary>
        /// <param name="threads">executing threads</param>
        internal static void ThreadStatusMonitorLog(params Thread[] threads)
        {
            //monitor lock status
            new Thread(() =>
            {
                while (threads.Any(t => t.ThreadState != ThreadState.Stopped))
                {
                    //write . per second
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
            }).Start();
        }

    }
}
