using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IAuthorizationService authorizationService;

        public OrdersController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET api/customers/10/orders

        [HttpGet("/api/customers/{customerId}/orders")]
        public IActionResult GetByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Order order = orderService.Get(id);

            

            var result = await authorizationService.AuthorizeAsync(this.User, order, "TheSameAuthor");

            if (result.Succeeded)
            {
                return Ok(order);
            }
            else
            {
                return Forbid();
            }
        }
    }
}
