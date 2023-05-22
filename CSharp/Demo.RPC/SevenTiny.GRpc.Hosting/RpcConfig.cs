using Grpc.Core;
using SevenTiny.GRpc.Implement;
using SevenTiny.GRpc.Protocol;
using System;

namespace SevenTiny.GRpc.Hosting
{
    public static class RpcConfig
    {
        private static Server _server;

        public static void Start()
        {
            _server = new Server
            {
                Services =
                {
                    BusinessService.BindService(new BusinessServiceImpl()),
                    MsgService.BindService(new MsgServiceImpl())
                },
                Ports = { new ServerPort("localhost", 40001, ServerCredentials.Insecure) }
            };
            _server.Start();

            Console.WriteLine("grpc ServerListening On Port 40001");
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            _server?.ShutdownAsync().Wait();
        }
    }
}