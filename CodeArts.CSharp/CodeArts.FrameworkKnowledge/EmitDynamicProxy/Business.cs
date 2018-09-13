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
        int GetAge(int age);
    }

    public class Business : IBusiness
    {
        public int GetAge(int age)
        {
            return age;
        }

        public void Test()
        {
            Trace.WriteLine("this a test ...");
        }
    }
}
