using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CSharp.System_Threading.SynchronousConstruction.Primitive.UserMode
{
    internal class Volatile_Test
    {
        private volatile static int vField1;

        public static void SetField(int a)
        {
            vField1 = a;
        }
    }
}
