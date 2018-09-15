using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using System;
using System.Diagnostics;

namespace Test.FrameworkKnoledge.EmitDynamicProxy
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InterceptorAttribute : InterceptorBaseAttribute
    {
        public override object Invoke(object @object, string method, object[] parameters)
        {
            Trace.WriteLine(string.Format("interceptor does something before invoke [{0}]...", @method));

            object obj = null;

            try
            {
                obj = base.Invoke(@object, method, parameters);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            Trace.WriteLine(string.Format("interceptor does something after invoke [{0}]...", @method));

            return obj;
        }
    }
}
