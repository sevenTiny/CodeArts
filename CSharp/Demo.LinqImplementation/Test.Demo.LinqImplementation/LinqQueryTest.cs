using Demo.LinqImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Test.Demo.LinqImplementation
{
    public class LinqQueryTest
    {
        [Fact]
        public void Where()
        {
            IDemoCollection<DemoModel> collection = new DemoCollection<DemoModel>();
            var query = collection.AsQueryable();
            query = query.Where(t => t.Age < 555);
            query = query.Where(t => t.Age > 333);
            var result = query.Select(t => t.Age).ToList().FirstOrDefault();

            /**
             * 结论：
             * 通过实现IQueryable的几个类型，在ToList方法会调用该类型的方法
             * 可以通过单元测试调试了解整个过程
             * 2019年4月26日15点58分
             * */
        }
    }
}
