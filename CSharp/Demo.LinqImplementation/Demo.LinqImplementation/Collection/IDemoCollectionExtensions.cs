using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.LinqImplementation
{
    public static class IDemoCollectionExtensions
    {
        public static IDemoQueryable<T> AsQueryable<T>(this IDemoCollection<T> collection)
        {
            var provider = new DemoQueryProviderImp<T>(collection);
            return new DemoQueryableImp<T>(provider);
        }
    }
}
