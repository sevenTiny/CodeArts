using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.LinqImplementation
{
    public interface IDemoQueryable : IQueryable
    {

    }

    public interface IDemoQueryable<T> : IDemoQueryable, IQueryable, IQueryable<T>
    {

    }
}
