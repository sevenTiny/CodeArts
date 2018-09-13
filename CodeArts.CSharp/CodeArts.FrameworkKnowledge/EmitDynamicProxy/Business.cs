using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    public interface IBusiness
    {
        void Test();
    }

    public class Business : IBusiness
    {
        public virtual void Test()
        {
            Trace.WriteLine("this a test ...");
        }
    }
}
