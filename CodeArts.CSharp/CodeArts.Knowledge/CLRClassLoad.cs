using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeArts.Knowledge
{
    public class CLRClassLoad
    {
        public static CLRClassLoad Instance = new CLRClassLoad();

        public void Method()
        {
            ClassA.Instance.Log();
        }
    }

    public class ClassA
    {
        public static ClassA Instance = new ClassA();

        public void Log()
        {
            Trace.WriteLine("ClassA->Log()");
        }
    }
}
