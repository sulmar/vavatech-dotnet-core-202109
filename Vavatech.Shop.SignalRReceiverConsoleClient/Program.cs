using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.SignalRReceiverConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Receiver!");

            const string url = "https://localhost:5001/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            // dotnet add package Microsoft.AspNetCore.SignalR.Protocols.MessagePack 

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .AddMessagePackProtocol()
                .Build();

            connection.On<Customer>("YouHaveGotNewCustomer", customer => Console.WriteLine($"Received {customer.FullName}"));

            Console.WriteLine($"Connecting {url}");

            await connection.StartAsync();

            Console.WriteLine($"Connected {connection.ConnectionId}");


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();




        }
    }
}
