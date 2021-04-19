using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DumpAnalysis.Problems
{
    class HighThreadCount
    {
        public static void Execute()
        {
            Task[] tasks = Enumerable.Range(0, 5).Select(t => Task.Factory.StartNew(() =>
               {
                   Thread.Sleep(1000);
                   Console.WriteLine("ThreadID:" + Thread.CurrentThread.ManagedThreadId);
               })).ToArray();

            Task.WaitAll(tasks);
        }
    }
}
