using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet60
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //InterlockedTest.PerformanceAnalysis();

            //采用monitor执行自增
            //InterlockedTest.PerformanceAnalysis_Monitor();

            //采用cas执行自增
            InterlockedTest.PerformanceAnalysis_Increament();
        }
    }
}
