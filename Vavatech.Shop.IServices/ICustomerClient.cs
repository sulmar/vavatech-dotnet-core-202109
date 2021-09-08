using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.IServices
{
    public interface ICustomerClient
    {
        Task YouHaveGotNewCustomer(Customer customer);
        Task Pong(string message);
    }
}
