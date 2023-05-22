using DapperMySql;
using SevenTiny.Bantina;
using System;
using System.Diagnostics;
using Xunit;

namespace Test.DapperMySql
{
    public class QueryTest
    {
        [Fact]
        public void QueryAll()
        {
            var result = new QueryService().GetList();
        }

        [Fact]
        public void Performance()
        {
            var time = StopwatchHelper.Caculate(() =>
            {
                new QueryService().Performance_All(100);
            }).TotalMilliseconds;
            Trace.WriteLine($"expenditure:" + time);
            //13514.7077  13098.4626
        }
    }
}
