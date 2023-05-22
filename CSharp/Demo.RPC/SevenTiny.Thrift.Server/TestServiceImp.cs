using System;
using System.Collections.Generic;
using System.Text;
using SevenTiny.Thrift.Contract;

namespace SevenTiny.Thrift.Server
{
    class TestServiceImp : TestService.Iface
    {
        public TestResult Save(TestArgs trxn)
        {
            return TestResult.SUCCESS;
        }
    }
}