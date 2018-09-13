using System;
using CodeArts.FrameworkKnowledge;
using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.FrameworkKnoledge
{
    [TestClass]
    public class EmitDynamicProxyTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Business business = DynamicProxy.Inject<Business>();
            business.Test();
            //Business.Instance.Test();
        }
    }
}
