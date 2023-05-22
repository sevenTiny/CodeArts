using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Demo.LinqImplementation
{
    public class DemoQueryableImp<T> : IDemoQueryable<T>
    {
        private IQueryProvider _queryProvider;
        private readonly Expression _expression;
        public DemoQueryableImp(IDemoQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
            _expression = Expression.Constant(this, typeof(IDemoQueryable<T>));

        }
        public DemoQueryableImp(IDemoQueryProvider queryProvider, Expression expression)
        {
            _queryProvider = queryProvider ?? throw new ArgumentException("can not be null", nameof(queryProvider));
            _expression = expression ?? throw new ArgumentException("can not be null", nameof(expression));
        }

        public Type ElementType => typeof(T);

        public Expression Expression => _expression;

        public IQueryProvider Provider => _queryProvider;

        public IEnumerator<T> GetEnumerator()
        {
            var results = (IEnumerable<T>)_queryProvider.Execute(_expression);
            return results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
