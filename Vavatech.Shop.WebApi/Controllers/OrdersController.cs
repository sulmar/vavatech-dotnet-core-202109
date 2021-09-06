using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.Shop.WebApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        // GET api/customers/10/orders

        [HttpGet("/api/customers/{customerId}/orders")]
        public IActionResult Get(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
