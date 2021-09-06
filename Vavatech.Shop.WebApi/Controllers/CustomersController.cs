using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using Vavatech.Shop.Models.SearchCritierias;

namespace Vavatech.Shop.WebApi.Controllers
{
    [Route("api/[controller]")]    // <- prefix
    [Route("api/klienci")]    // <- prefix
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET api/customers - endpoint (adres końcowy)
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = customerService.Get();

        //    return Ok(customers);
        //}

        // GET api/customers/{id} - endpoint (adres końcowy)
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET api/customers/{pesel}

        [HttpGet("{pesel:length(11):pesel}")]
        public IActionResult Get(string pesel)
        {
            var customer = customerService.GetByPesel(pesel);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }


        // GET api/customers?Pesel={pesel}
        //[HttpGet]
        //public IActionResult Get(string pesel)
        //{
        //    var customer = customerService.GetByPesel(pesel);

        //    if (customer == null)
        //        return NotFound();

        //    return Ok(customer);
        //}


        // GET api/customers&city=Warszawa&street=Dworcowa
        [HttpGet]
        public IActionResult Get([FromQuery] CustomerSearchCriteria searchCriteria)
        {
            var customers = customerService.Get(searchCriteria);

            return Ok(customers);
        }

    }
}
