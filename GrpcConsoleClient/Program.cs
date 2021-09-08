using Grpc.Net.Client;
using System;
using Vavatech.Shop.GrpcServer;

namespace GrpcConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Client!");

            const string url = "https://localhost:5001";

            GrpcChannel channel = GrpcChannel.ForAddress(url);

            var client = new TrackingService.TrackingServiceClient(channel);

            
        }
    }
}
