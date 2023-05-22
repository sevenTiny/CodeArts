using System.Collections.Generic;

namespace Demo.CSharpScript.Models
{
    /// <summary>
    /// 测试实体
    /// </summary>
    public class DemoModel
    {
        public int ID { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        /// <summary>
        /// 测试数据
        /// </summary>
        /// <returns></returns>
        public static List<DemoModel> GetDemoDatas()
        {
            var list = new List<DemoModel>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new DemoModel { ID = i, Age = i, Name = $"7tiny_{i}", Desc = $"第{i}条测试数据" });
            }
            return list;
        }
    }
}
