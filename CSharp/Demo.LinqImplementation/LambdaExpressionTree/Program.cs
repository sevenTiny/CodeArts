using LambdaExpressionTree.Translate;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTree
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        ////BuildExpressionTree.Instance.GetQuery("zc",28,"coding").Build();
    //        //Expression<Func<Person, bool>> expression = d => d.Age > 20 && new[] { 1, 2, 3 }.Contains(d.Age) || d.Name == "zc" && d.Birthday < DateTime.Now;
    //        //ExpressionAnalyze analyzer = new ExpressionAnalyze();
    //        //Expression result = analyzer.ResolveExpression(expression);

    //        //Console.WriteLine("sql:" + analyzer.Sql);

    //        //BuildExpressionTree.Instance.GetQuery("zc",28,"coding").Build();
    //        Expression<Func<Person, bool>> expression = d => d.Age > 20 && new[] { 1, 2, 3 }.Contains(d.Age) || d.Name == "zc" && d.Birthday < DateTime.Now;
    //        ExpressionPrinter analyzer = new ExpressionPrinter();
    //        Expression result = analyzer.Visit(expression);

    //        Console.WriteLine("sql:" + analyzer.Sql);

    //        Console.ReadKey();



    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            using (NorthwindDbContext db = new NorthwindDbContext())
            {
                IQueryable<OperateTest> query = db.OperateTests.Where(c => c.IntKey < 30);
                //IQueryable<OperateTest> query = db.OperateTests.Where(c => c.IntKey < 30).Where(t => t.IntKey > 20);

                //var sql = query.ToString();

                //var list = query.ToList();

                var fir = query.FirstOrDefault();

                Console.ReadLine();
            }
        }
    }
}
