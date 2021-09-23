using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Threading
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void ContinueWith_OnlyOnFaulted()
        {
            Task.Run(() =>
            {
                throw new ArgumentException("throw an exception");
            }).ContinueWith(r =>
            {
                Trace.WriteLine("got an exception -> " + r.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);


            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Trace.WriteLine($"waiting...{i}");
            }
        }
    }
}
