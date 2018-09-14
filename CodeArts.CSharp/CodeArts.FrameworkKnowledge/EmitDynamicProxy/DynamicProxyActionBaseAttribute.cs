using System;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DynamicProxyActionBaseAttribute : Attribute
    {
        public virtual void Before()
        {
        }
    }
}
