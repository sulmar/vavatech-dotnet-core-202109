using System;
using System.Threading.Tasks;
using Vavatech.Shop.WebApiConsoleClient.OpenAPIService;

namespace Vavatech.Shop.WebApiConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Client client = new Client("https://localhost:5001", new System.Net.Http.HttpClient());

            var customer = await client.ApiCustomersGetAsync(10);

            Console.WriteLine(customer.FullName);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
