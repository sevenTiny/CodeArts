using Grpc.Core;
using SevenTiny.GRpc.Protocol;
using SevenTiny.GRpc.Protocol.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenTiny.GRpc.Implement
{
    public class BusinessServiceImpl: BusinessService.BusinessServiceBase
    {
        public BusinessServiceImpl() { }
        public override async Task<OperateResult> Test(RequestArgs request, ServerCallContext context)
        {
            OperateResult result = new OperateResult();
            result.Result = "33333" + request.Arg1;
            return result;
        }
        public override async Task<OperateResult> GetShopName(RequestArgs request, ServerCallContext context)
        {
            OperateResult result = new OperateResult();
            result.Result = "7tiny result:" + request.Arg1;
            return result;
        }
    }
}
