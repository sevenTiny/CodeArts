using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using System;
using System.Diagnostics;

namespace Test.FrameworkKnoledge.EmitDynamicProxy
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DynamicProxyActionAttribute : DynamicProxyActionBaseAttribute
    {
        public override void Before()
        {
            Trace.WriteLine("Attribute Before");
        }
    }
}