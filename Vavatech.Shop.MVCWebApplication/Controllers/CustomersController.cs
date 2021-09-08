using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;

namespace Vavatech.Shop.MVCWebApplication.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            var customers = customerService.Get();

            return View(customers);
        }

        public IActionResult Edit(int id)
        {
            var customer = customerService.Get(id);
         
            return View(customer);

        }
    }
}
