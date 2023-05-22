using SevenTiny.Thrift.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevenTiny.Thrift.Server
{
    class BusinessServiceImp : BusinessService.Iface
    {
        public string Test123(string arg)
        {
            return "arg:" + arg;
        }
    }
}
