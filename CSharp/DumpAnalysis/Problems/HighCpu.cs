using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DumpAnalysis.Problems
{
    public class HighCpu
    {
        public static void Execute()
        {
            while (true)
            {
                Enumerable.Repeat(1, 1000).AsParallel().ForAll(t => Thread.SpinWait(100));
            }
        }
    }
}
