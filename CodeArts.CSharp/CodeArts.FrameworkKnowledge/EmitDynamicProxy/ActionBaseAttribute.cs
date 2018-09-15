using System;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ActionBaseAttribute : Attribute
    {
        public virtual void Before(string @method, object[] parameters) { }

        public virtual object After(string @method, object result) { return result; }
    }
}
