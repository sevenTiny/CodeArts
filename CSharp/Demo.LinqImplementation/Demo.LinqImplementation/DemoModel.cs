using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Demo.LinqImplementation
{
    public class DemoModel
    {
        public DemoModel() { }
        public DemoModel(string name, int age) { Name = name; Age = age; }
        public string Name { get; set; }
        public int Age { get; set; }

        public static List<DemoModel> DemoModels =>
        new List<DemoModel>
        {
            new DemoModel("111",111),
            new DemoModel("222",222),
            new DemoModel("333",333),
            new DemoModel("444",444),
            new DemoModel("555",555),
            new DemoModel("666",666),

        };

        public static List<DemoModel> GetDemoModelsByCondition(Expression expression)
        {
            var func = Expression.Lambda<Func<IDemoQueryable<DemoModel>, bool>>(expression).Compile();
            return null;
            //return DemoModel.DemoModels.Where(func).ToList();
        }
    }
}
