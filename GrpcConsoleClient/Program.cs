using Bogus;
using Grpc.Net.Client;
using Grpc.Core;
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

            // await SendLocations(client);

            var request = new SubscribeRequest { Speed = 100 };

            var streamingLocations = client.Subscribe(request);

            // add using Grpc.Core
            await foreach (var location in streamingLocations.ResponseStream.ReadAllAsync())
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"ALERT {location.Speed} {location.Name}");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();

        }

        private static async Task SendLocations(TrackingService.TrackingServiceClient client)
        {
            // dotnet add package Bogus
            var locations = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Name, f => f.Vehicle.Model())
                .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude())
                .RuleFor(p => p.Longitude, f => (float)f.Address.Longitude())
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 140))
                .RuleFor(p => p.Direction, f => f.Random.Float())
                .GenerateForever();

            foreach (var location in locations)
            {
                Console.Write($"Sending {location.Latitude} {location.Longitude} {location.Speed} {location.Direction}");
                var response = await client.AddLocationAsync(location);

                Console.WriteLine($"{response.IsConfirmed}  Send.");

                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }
        }
    }
}
