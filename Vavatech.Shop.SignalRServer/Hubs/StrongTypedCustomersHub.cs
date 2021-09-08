using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.SignalRServer.Hubs
{
    public class StrongTypedCustomersHub : Hub<ICustomerClient>, ICustomerServer
    {
        private readonly ILogger<CustomersHub> logger;

        public StrongTypedCustomersHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public async Task SendCustomer(Customer customer)
        {
            logger.LogInformation("YouHaveGotNewCustomer {0}", customer.FullName);

            await Clients.Others.YouHaveGotNewCustomer(customer);

            await Clients.Group("GrupaA").YouHaveGotNewCustomer(customer);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.Pong(message);
        }
    }
}
