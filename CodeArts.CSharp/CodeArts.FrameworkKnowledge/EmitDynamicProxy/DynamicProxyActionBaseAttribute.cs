using System;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DynamicProxyActionBaseAttribute : Attribute
    {
        public virtual void Before()
        {
        }
    }
}
