using CodeArts.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Knowledge
{
    public class CLRClassLoadTest
    {
        [Fact]
        public void Test()
        {
            CLRClassLoad.Instance.Method();
        }
    }
}
