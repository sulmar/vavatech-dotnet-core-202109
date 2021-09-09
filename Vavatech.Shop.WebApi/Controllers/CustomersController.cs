using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using Vavatech.Shop.Models.SearchCritierias;

namespace Vavatech.Shop.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    // <- prefix
    // [Route("api/klienci")]    // <- prefix
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<CustomersController> logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
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
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> Get(int id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET api/customers/{pesel}

        /// <summary>
        /// Pobierz klienta na podstawie numeru PESEL
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns></returns>
        [HttpGet("{pesel:length(11):pesel}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Customer> Get(string pesel)
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

        [Authorize(Roles = "Developer,Trainer")]
        [HttpGet]
        public ActionResult<Customer[]> Get([FromQuery] CustomerSearchCriteria searchCriteria)
        {

            //if (!this.User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}

            if (this.User.IsInRole("Trainer"))
            {

            }

            if (this.User.HasClaim(c=>c.Type=="kat" && c.Value =="A"))
            {

            }

            string email = this.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            var customers = customerService.Get(searchCriteria);

            // logger.LogInformation($"Generated {customers.Count()} customers");
            logger.LogInformation("Generated {0} customers", customers.Count());

            return Ok(customers);
        }

        // POST api/customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] Customer customer)
        {
            customerService.Add(customer);

            // return Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
        }

        // PUT api/customers/{id}
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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

        // dotnet add package Microsoft.AspNetCore.JsonPatch

        // http://jsonpatch.com
        // PATCH api/customers/{id}
        // Content-Type: application/json-patch+json
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Customer> patchCustomer)
        {
            Customer existingCustomer = customerService.Get(id);

            if (existingCustomer == null)
                return NotFound();

            patchCustomer.ApplyTo(existingCustomer);

            return NoContent();
        }


        [Authorize(Policy = "IsWomanAdult")]
        
        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            Customer existingCustomer = customerService.Get(id);

            if (existingCustomer == null)
                return NotFound();

            customerService.Remove(id);

            return Ok();
        }

        [AllowAnonymous]
        [HttpHead("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Head(int id)
        {
            Customer existingCustomer = customerService.Get(id);

            if (existingCustomer == null)
                return NotFound();

            return Ok();
        }

        public IActionResult Batch(Customer[] customers)
        {
            // TODO: create job

            return Accepted();
        }


        // GET api/orders?customerId=10

        // GET api/customers/10/orders

        // GET api/customers/10/orders/last

        // GET api/customers/10/orders/2020
        // GET api/customers/10/orders/2020/09

    }
}
