using Dapper;
using DapperMySql.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperMySql
{
    public class QueryService
    {
        private const string ConnectionString = "连接字符串";
        public List<OperateTest> GetList()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                return connection.Query<OperateTest>("select * from OperateTest").AsList();
            }
        }

        public void Performance_All(int count)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                for (int i = 0; i < count; i++)
                {
                    var re = connection.Query<OperateTest>("select * from OperateTest").AsList();
                }
            }
        }

        public OperateTest GetOne()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                return connection.QuerySingle<OperateTest>(" select * from User where id=@id", new { id = 1002 });
            }
        }

        public void Add(OperateTest operateTest)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                //sql 操作
                var rInt = connection.Execute(" insert User (ID,Name) values (@id,'javc')", new { id = new Random().Next(100, 200) });
                rInt = connection.Execute(" update User set name=@name where id=1002", new { name = "hellon" });
            }
        }
    }
}
