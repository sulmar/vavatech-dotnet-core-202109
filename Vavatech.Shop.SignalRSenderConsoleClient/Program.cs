using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Vavatech.Shop.Fakers;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.SignalRSenderConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Sender!");

            const string url = "https://localhost:5001/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            // dotnet add package Microsoft.AspNetCore.SignalR.Protocols.MessagePack 

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .AddMessagePackProtocol()
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;

            Console.WriteLine($"Connecting {url}");

            await connection.StartAsync();

            Console.WriteLine($"Connected {connection.ConnectionId}");

            Faker<Customer> faker = new CustomerFaker(new AddressFaker(), new PasswordHasher<Customer>());

            var customers = faker.GenerateForever();

            foreach (var customer in customers)
            {
                Console.Write($"Sending customer {customer.FullName}...");

                await connection.SendAsync(nameof(ICustomerServer.SendCustomer), customer);

                Console.WriteLine(" Sent.");

                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }

        private static Task Connection_Reconnected(string arg)
        {
            Console.WriteLine("Reconnected.");

            // TODO: send buffered data
            
            return Task.CompletedTask;
        }

        private static Task Connection_Reconnecting(Exception arg)
        {
            Console.WriteLine("Reconnecting...");

            // TODO: buffer data

            return Task.CompletedTask;
        }
    }
}
