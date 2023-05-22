using Grpc.Core;
using SevenTiny.GRpc.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenTiny.GRpc.Implement
{
    public class MsgServiceImpl : MsgService.MsgServiceBase
    {
        public MsgServiceImpl()
        {
        }

        public override async Task<GetMsgSumReply> GetSum(GetMsgNumRequest request, ServerCallContext context)
        {
            var result = new GetMsgSumReply();

            result.Sum = request.Num1 + request.Num2;

            return result;
        }
    }
}
