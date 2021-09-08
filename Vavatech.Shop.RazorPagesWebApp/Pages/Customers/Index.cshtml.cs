using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.RazorPagesWebApp.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        private readonly ICustomerService customerService;

        public IndexModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void OnGet()
        {
            Message = "Hello Razor Pages!";

            Customers = customerService.Get();
        }
    }
}
