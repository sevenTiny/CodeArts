using CodeArts.FrameworkKnowledge.ThrowException;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.FrameworkKnowledge
{
    [TestClass]
    public class ExceptionTest
    {
        [TestMethod]
        public void Test()
        {
            new ThrowExceptionPerformance().ThrowException();
        }
    }
}
