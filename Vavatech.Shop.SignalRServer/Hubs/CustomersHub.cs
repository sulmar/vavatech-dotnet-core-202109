using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.SignalRServer.Hubs
{
    public class CustomersHub : Hub
    {
        private readonly ILogger<CustomersHub> logger;

        public CustomersHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            logger.LogInformation("Connected {0}", this.Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task SendCustomer(Customer customer)
        {
            logger.LogInformation("YouHaveGotNewCustomer {0}", customer.FullName);

            await Clients.Others.SendAsync("YouHaveGotNewCustomer", customer);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.SendAsync("Pong", message);
        }
    }
}
