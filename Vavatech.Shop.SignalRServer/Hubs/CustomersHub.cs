using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.SignalRServer.Hubs
{
   // [Authorize]
    public class CustomersHub : Hub
    {
        private readonly ILogger<CustomersHub> logger;

        public CustomersHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            logger.LogInformation("Connected {0}", this.Context.ConnectionId);

            if (this.Context.User.Identity.IsAuthenticated)
            {
                string companyName = Context.User.FindFirst(c => c.Type == "Company").Value;

                await Groups.AddToGroupAsync(Context.ConnectionId, companyName);
            }

            await base.OnConnectedAsync();
        }

        //public async Task JoinRoom(string roomName)
        //{
        //    await this.Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        //}

        public async Task SendCustomer(Customer customer)
        {
            logger.LogInformation("YouHaveGotNewCustomer {0}", customer.FullName);
            
            await Clients.Others.SendAsync("YouHaveGotNewCustomer", customer);

            await Clients.Group("GrupaA").SendAsync("YouHaveGotNewCustomer {0}", customer);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.SendAsync("Pong", message);
        }
    }
}
