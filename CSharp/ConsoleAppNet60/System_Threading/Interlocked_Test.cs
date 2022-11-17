using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet60.System_Threading
{
    internal class Interlocked_Test
    {
        /// <summary>
        /// end point
        /// </summary>
        public static void Run()
        {
            PerformanceAnalysis_Increament();
        }

        /// <summary>
        /// thread safe increament
        // </summary>
        static void Increament()
        {
            var j = 0;

            Task.WaitAll(
                Enumerable.Range(0, 50)
                .Select(t =>
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 2000; i++)
                        {
                            Interlocked.Increment(ref j);
                        }
                    }
                ))
                .ToArray()
                );

            Console.WriteLine($"multi thread increament result={j}");
            //result=100000
        }

        static void IncreamentPerformance()
        {
            //lock method

            var locker = new object();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var j1 = 0;

            Task.WaitAll(
                Enumerable.Range(0, 50)
                .Select(t =>
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 200000; i++)
                        {
                            lock (locker)
                            {
                                j1++;
                            }
                        }
                    }
                ))
                .ToArray()
                );

            Console.WriteLine($"Monitor lock，result={j1},elapsed={stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();

            //Increment method

            var j2 = 0;

            Task.WaitAll(
                Enumerable.Range(0, 50)
                .Select(t =>
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 200000; i++)
                        {
                            Interlocked.Increment(ref j2);
                        }
                    }
                ))
                .ToArray()
                );

            stopwatch.Stop();

            Console.WriteLine($"Interlocked.Increment，result={j2},elapsed={stopwatch.ElapsedMilliseconds}");
        }

        static void PerformanceAnalysis_Monitor()
        {
            //lock method

            var locker = new object();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var j = 0;

            Task.WaitAll(
                Enumerable.Range(0, 50)
                .Select(t =>
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 200000; i++)
                        {
                            lock (locker)
                            {
                                j++;
                            }
                        }
                    }
                ))
                .ToArray()
                );

            stopwatch.Stop();

            Console.WriteLine($"Monitor lock，result={j},elapsed={stopwatch.ElapsedMilliseconds}");
        }

        static void PerformanceAnalysis_Increament()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var j = 0;

            Task.WaitAll(
                Enumerable.Range(0, 50)
                .Select(t =>
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 200000; i++)
                        {
                            Interlocked.Increment(ref j);
                        }
                    }
                ))
                .ToArray()
                );

            stopwatch.Stop();

            Console.WriteLine($"Interlocked.Increment，result={j},elapsed={stopwatch.ElapsedMilliseconds}");
        }
    }
}
