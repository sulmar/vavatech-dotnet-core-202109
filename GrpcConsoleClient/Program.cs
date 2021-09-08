using Bogus;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using Vavatech.Shop.GrpcServer;

namespace GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Client!");

            const string url = "https://localhost:5001";

            GrpcChannel channel = GrpcChannel.ForAddress(url);

            var client = new TrackingService.TrackingServiceClient(channel);

            // dotnet add package Bogus
            AddLocationRequest location = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Name, f => f.Vehicle.Model())
                .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude())
                .RuleFor(p => p.Longitude, f => (float)f.Address.Longitude())
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 140))
                .RuleFor(p => p.Direction, f => f.Random.Float())
                .Generate();

            Console.Write($"Sending {location.Latitude} {location.Longitude} {location.Speed} {location.Direction}");
            var response = await client.AddLocationAsync(location);

            Console.WriteLine($"{response.IsConfirmed}  Send.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            
        }
    }
}
