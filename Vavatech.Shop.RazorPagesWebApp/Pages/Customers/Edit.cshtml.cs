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
    public class EditModel : PageModel
    {
        private readonly ICustomerService customerService;

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public EditModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void OnGet()
        {
            Customer = customerService.Get(Id);
        }

        public IActionResult OnPost()
        {
            customerService.Update(Customer);

            return RedirectToPage("./Index");
        }
    }
}
