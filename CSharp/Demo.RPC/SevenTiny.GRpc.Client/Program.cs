using Grpc.Core;
using SevenTiny.GRpc.Protocol;
using SevenTiny.GRpc.Protocol.Model;
using System;
using System.Diagnostics;

namespace SevenTiny.GRpc.Client
{
    class Program
    {
        private static Channel _channel = new Channel("127.0.0.1:40001", ChannelCredentials.Insecure);
        private static BusinessService.BusinessServiceClient _client;

        static void Main(string[] args)
        {
            _client = new BusinessService.BusinessServiceClient(_channel);

            RequestArgs argg = new RequestArgs();
            argg.Arg1 = "7tiny";

            Stopwatch stop = new Stopwatch();
            stop.Start();
            for (int i = 0; i < 10000; i++)
            {
                _client.Test(argg);
            }
            stop.Stop();
            Console.WriteLine($"万次调用耗时 {stop.ElapsedMilliseconds}ms.");
            var result2 = _client.GetShopName(argg).Result;
            //万次调用2823ms

            Console.WriteLine("任意键退出...");
            Console.ReadKey();
        }
    }
}