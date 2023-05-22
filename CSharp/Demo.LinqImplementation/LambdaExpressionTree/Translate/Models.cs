using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTree.Translate
{
    public partial class OperateTest
    {
        public int Id { get; set; }
        public int Key2 { get; set; }
        public string StringKey { get; set; }
        public int IntKey { get; set; }
        public int? IntNullKey { get; set; }
        public DateTime? DateNullKey { get; set; }
        public DateTime? DateTimeNullKey { get; set; }
        public double? DoubleNullKey { get; set; }
        public float? FloatNullKey { get; set; }
    }

    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime? SchoolTime { get; set; }
    }

    public class NorthwindDbContext : DbContext
    {
        public DbSet<OperateTest> OperateTests;
        public DbSet<Student> Students;

        public NorthwindDbContext()
        {
            this.OperateTests = new DbSet<OperateTest>(QueryProvider);
            this.Students = new DbSet<Student>(QueryProvider);
        }
    }

    public abstract class DbContext : IDisposable
    {
        protected DbConnection Connection { get; set; }
        protected QueryProvider QueryProvider { get; set; }
        protected DbContext()
        {
            Connection = new MySql.Data.MySqlClient.MySqlConnection(Consts.Conn);
            QueryProvider = new DbSetQueryProvider(Connection);
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
                Connection.Dispose();
            }
        }
    }
}
