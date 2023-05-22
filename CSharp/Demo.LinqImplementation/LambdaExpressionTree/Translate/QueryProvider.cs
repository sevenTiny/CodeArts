using System.Linq;
using System.Linq.Expressions;

namespace LambdaExpressionTree.Translate
{
    public abstract class QueryProvider : IQueryProvider
    {
        protected QueryProvider() { }

        IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression)
        {
            return GetQueryable<T>(expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            return GetQueryable(expression);
        }

        T IQueryProvider.Execute<T>(Expression expression)
        {
            return (T)Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return Execute(expression);
        }

        public abstract IQueryable<T> GetQueryable<T>(Expression expression);
        public abstract IQueryable GetQueryable(Expression expression);
        public abstract string GetQueryText(Expression expression);
        public abstract object Execute(Expression expression);
    }
}
