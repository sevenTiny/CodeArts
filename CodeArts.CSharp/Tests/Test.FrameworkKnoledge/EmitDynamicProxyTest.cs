using System;
using CodeArts.FrameworkKnowledge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.FrameworkKnoledge
{
    [TestClass]
    public class EmitDynamicProxyTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Business business = Proxy.Of<Business>();
            business.Test();
            //Business.Instance.Test();

            BusinessProxy p = new BusinessProxy();
            p.Test();
        }
    }
}
