using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using System.Diagnostics;

namespace Test.FrameworkKnoledge.EmitDynamicProxy
{
    public class OperateResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public interface IBusinessClass
    {
        void Test();
        int GetAge(int age);
        OperateResult GetOperateResult(int code,string message);
    }

    public class BusinessClass : IBusinessClass
    {
        public static IBusinessClass Instance = DynamicProxy.Create<IBusinessClass, BusinessClass, DynamicProxyInterceptor>();

        public int GetAge(int age)
        {
            return age;
        }

        public OperateResult GetOperateResult(int code, string message)
        {
            return new OperateResult { Code = code, Message = message };
        }

        [DynamicProxyAction]
        public void Test()
        {
            Trace.WriteLine("this a test ...");
        }
    }
}