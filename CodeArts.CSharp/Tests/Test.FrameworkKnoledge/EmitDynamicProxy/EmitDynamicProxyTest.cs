using System;
using CodeArts.FrameworkKnowledge;
using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.FrameworkKnoledge.EmitDynamicProxy
{
    [TestClass]
    public class EmitDynamicProxyTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var instance = BusinessClass.Instance;
            instance.Test();
            var result = instance.GetOperateResult(111, "333");
            int intResult = instance.GetAge(222);
        }
    }
}