using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace Demo.LinqImplementation
{
    public static class DemoQueryable
    {
        public static IDemoQueryable<TSource> Where<TSource>(this IDemoQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return (IDemoQueryable<TSource>)Queryable.Where(source, predicate);
        }
    }
}
