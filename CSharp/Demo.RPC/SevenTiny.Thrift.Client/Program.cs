using SevenTiny.Thrift.Contract;
using System;
using System.Diagnostics;
using Thrift.Protocol;
using Thrift.Transport;

namespace SevenTiny.Thrift.Client
{
    class Program
    {
        private const int port = 8885;
        static void Main(string[] args)
        {
            // RPC - use Thrift
            using (TTransport transport = new TSocket("localhost", port))
            {
                using (TProtocol protocol = new TBinaryProtocol(transport))
                {
                    using (var serviceClient = new BusinessService.Client(protocol))
                    {
                        transport.Open();

                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        for (int i = 0; i < 10000; i++)
                        {
                            serviceClient.Test123("7tiny");
                        }
                        watch.Stop();
                        Console.WriteLine($"万次调用耗时：{watch.ElapsedMilliseconds}");
                        //万次调用耗时3300ms
                    }

                    //如果不适用多路服务复合模式，直接将protocol放入Client()即可，无需取对应服务
                    //using (var businessService = new TMultiplexedProtocol(protocol, "BusinessService"))
                    //{
                    //    using (var serviceClient = new BusinessService.Client(businessService))
                    //    {
                    //        transport.Open();

                    //        Stopwatch watch = new Stopwatch();
                    //        watch.Start();
                    //        for (int i = 0; i < 10000; i++)
                    //        {
                    //        serviceClient.Test123("7tiny");
                    //        }
                    //        watch.Stop();
                    //        Console.WriteLine($"万次调用耗时：{watch.ElapsedMilliseconds}");
                    //        //万次调用耗时3300ms
                    //    }
                    //}
                }
            }
        }
    }
}