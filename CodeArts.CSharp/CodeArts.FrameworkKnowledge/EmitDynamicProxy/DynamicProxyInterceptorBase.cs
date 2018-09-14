namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    public class DynamicProxyInterceptorBase
    {
        public virtual object Invoke(object @object, string @method, object[] parameters)
        {
            return @object.GetType().GetMethod(@method).Invoke(@object, parameters);
        }
    }
}
