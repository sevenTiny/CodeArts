using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeArts.FrameworkKnowledge.ThrowException
{
    public class ThrowExceptionPerformance
    {
        public void ThrowException()
        {
            int executeTimes = 10000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < executeTimes; i++)
            {
                try
                {
                    throw new Exception("xxx");
                }
                catch (Exception)
                {
                    //do noting ...
                }
            }
            stopwatch.Stop();
            Trace.WriteLine($"{executeTimes} 次调用抛出异常耗时：{stopwatch.ElapsedMilliseconds}");
            //超出一分钟
        }
    }
}
