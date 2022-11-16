using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet60.System_Threading
{
    internal class VolatileTest
    {
        private volatile static int vField1;

        public static void SetField(int a)
        {
            vField1 = a;
        }
    }
}
