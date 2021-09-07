using ServiceStack;
using System;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.ApiConsoleClient
{
    class Program
    {
        // dotnet add package ServiceStack.HttpClient
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int customerId = 10;

            string url = $"https://localhost:5001/api/customers/{customerId}";

            var json = await url.GetJsonFromUrlAsync();

            var customer = json.FromJson<Customer>();


        }


    }
}
