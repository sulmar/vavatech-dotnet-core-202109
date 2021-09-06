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
    // [Route("api/klienci")]    // <- prefix
    public class CustomersController : ControllerBase
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
        [HttpGet("{id:int}", Name = "GetCustomerById")]
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

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            customerService.Add(customer);

            // return Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
        }

        // PUT api/customers/{id}
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            Customer existingCustomer = customerService.Get(id);

            if (existingCustomer == null)
                return NotFound();

            customerService.Update(customer);

            return Ok(customer);
        }

        // PATCH 

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Customer existingCustomer = customerService.Get(id);

            if (existingCustomer == null)
                return NotFound();

            customerService.Remove(id);

            return Ok();
        }


        // GET api/orders?customerId=10

        // GET api/customers/10/orders

        // GET api/customers/10/orders/last

        // GET api/customers/10/orders/2020
        // GET api/customers/10/orders/2020/09

    }
}
