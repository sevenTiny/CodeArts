using System;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace SevenTiny.Thrift.Server
{
    class Program
    {
        private const int port = 8885;
        static void Main(string[] args)
        {
            Console.WriteLine("[Welcome] PaymentService RPC Server is lanuched...");
            TServerTransport transport = new TServerSocket(port);

            var processor1 = new SevenTiny.Thrift.Contract.TestService.Processor(new TestServiceImp());
            var processor2 = new SevenTiny.Thrift.Contract.BusinessService.Processor(new BusinessServiceImp());

            //var processorMulti = new TMultiplexedProcessor();
            //processorMulti.RegisterProcessor("TestService", processor1);
            //processorMulti.RegisterProcessor("BusinessService", processor2);

            //TServer server = new TThreadedServer(processorMulti, transport);
            TServer server = new TThreadedServer(processor2, transport);

            // lanuch
            server.Serve();
        }
    }
}