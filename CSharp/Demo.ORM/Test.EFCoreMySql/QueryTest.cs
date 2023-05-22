using EFCoreMySql.Entity;
using SevenTiny.Bantina;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace Test.EFCoreMySql
{
    public class QueryTest
    {
        [Fact]
        public void QueryAll()
        {
            using (var context = new SevenTinyTestContext())
            {
                var list1 = context.Student.ToList();
                var list2 = context.OperateTest.ToList();
            }
        }

        [Fact]
        public void Performance()
        {
            using (var context = new SevenTinyTestContext())
            {
                var time = StopwatchHelper.Caculate(100, () =>
                {
                    Assert.True(context.OperateTest.ToList().Count > 0);
                }).TotalMilliseconds;
                Trace.WriteLine($"expenditure:" + time);
                //14000
            }
        }
    }
}
