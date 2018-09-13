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
            IBusiness business = DynamicProxy.Inject<IBusiness, Business>();
            business.Test();
            business.GetAge(888);
            //Business.Instance.Test();

            BusinessProxy b = new BusinessProxy();
            b.Test();
            b.GetAge(888);
        }
    }
}