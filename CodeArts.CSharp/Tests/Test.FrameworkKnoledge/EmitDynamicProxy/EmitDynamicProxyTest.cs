using System;
using System.Collections.Generic;
using System.Diagnostics;
using CodeArts.FrameworkKnowledge;
using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.FrameworkKnoledge.EmitDynamicProxy
{
    [TestClass]
    public class EmitDynamicProxyTest
    {
        [TestMethod]
        public void Performance1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000000; i++)
            {
                var instance = BusinessClass.Instance;
                instance.Test();
                var result = instance.GetOperateResult(111, "333");
                int intResult = instance.GetInt(222);
            }

            stopwatch.Stop();
            Trace.WriteLine($"动态代理调用耗时:{stopwatch.ElapsedMilliseconds}");
            //百万次调用耗时 1598 ms
        }
        [TestMethod]
        public void Performance2()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000000; i++)
            {
                var instance = BusinessClass.Instance2;
                instance.Test();
                var result = instance.GetOperateResult(111, "333");
                int intResult = instance.GetInt(222);
            }

            stopwatch.Stop();
            Trace.WriteLine($"直接调用耗时:{stopwatch.ElapsedMilliseconds}");
            //百万次调用耗时 102 ms
        }

        [TestMethod]
        [Description("实现方式动态代理")]
        public void FaultTolerantOfRealize()
        {
            IBusinessClass Instance = DynamicProxy.CreateProxyOfRealize<IBusinessClass, BusinessClass, DynamicProxyInterceptor>();

            Instance.Test();
            Instance.GetBool(false);
            Instance.GetInt(123);
            Instance.GetFloat(123f);
            Instance.GetDouble(123.123);
            Instance.GetString("123");
            Instance.GetObject(null);
            Instance.GetOperateResult(123, "123");
            Instance.GetOperateResults(new List<OperateResult>());
            Instance.GetDecimal(123.123m);
            Instance.GetDateTime(DateTime.Now);
        }

        [TestMethod]
        [Description("继承方式动态代理")]
        public void FualtTolerantOfInherit()
        {
            IBusinessClass Instance = DynamicProxy.CreateProxyOfInherit<BusinessClassVirtual, DynamicProxyInterceptor>();

            Instance.Test();
            Instance.GetBool(false);
            Instance.GetInt(123);
            Instance.GetFloat(123f);
            Instance.GetDouble(123.123);
            Instance.GetString("123");
            Instance.GetObject(null);
            Instance.GetOperateResult(123, "123");
            Instance.GetOperateResults(new List<OperateResult>());
            Instance.GetDecimal(123.123m);
            Instance.GetDateTime(DateTime.Now);
        }

        [TestMethod]
        public void ExceptionFilter()
        {
            IBusinessClass Instance = DynamicProxy.CreateProxyOfRealize<IBusinessClass, BusinessClass, DynamicProxyInterceptor>();
            Instance.ThrowException();
        }
    }
}