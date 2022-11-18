using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.CSharp.DumpAnalysis
{
    public class HighCpu
    {
        public static void Run()
        {
            Execute();

            while (true)
            {
                Console.WriteLine("waiting...");
                Thread.Sleep(1000);
            }
        }

        static void Execute()
        {
            var list = Enumerable.Range(1, 10000).ToArray().AsParallel();

            while (true)
            {
                var sum = list.Sum();
                var rev = list.Reverse().Sum();
                var avg = list.Average();
                var max = list.Max();
                var min = list.Min();
                var avg2 = list.Select(t => Math.PI * t).Average();
            }
        }
    }
}
