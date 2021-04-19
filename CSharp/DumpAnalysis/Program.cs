using DumpAnalysis.Problems;
using System;
using System.Threading;

namespace DumpAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //线程数过多
            //HighThreadCount.Execute();

            //CPU占用高
            HighCpu.Execute();

            while (true)
            {
                Console.WriteLine("waiting...");
                Thread.Sleep(1000);
            }
        }
    }
}
