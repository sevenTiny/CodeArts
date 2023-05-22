using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTree.Translate
{
    public class DbSetQueryProvider : QueryProvider
    {
        DbConnection connection;

        public DbSetQueryProvider(DbConnection connection)
        {
            this.connection = connection;
        }

        public override string GetQueryText(Expression expression)
        {
            return this.Translate(expression);
        }

        public override object Execute(Expression expression)
        {
            DbCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = this.Translate(expression);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            DbDataReader reader = cmd.ExecuteReader();
            Type elementType = TypeSystem.GetElementType(expression.Type);
            return Activator.CreateInstance(
                typeof(ObjectReader<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { reader },
                null);
        }

        private string Translate(Expression expression)
        {
            return new QueryTranslator().Translate(expression);
        }

        public override IQueryable<T> GetQueryable<T>(Expression expression)
        {
            return new DbSet<T>(this, expression);
        }

        public override IQueryable GetQueryable(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(DbSet<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }
    }
}
