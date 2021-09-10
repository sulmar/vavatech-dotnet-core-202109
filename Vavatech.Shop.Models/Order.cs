using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.Shop.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; } // Navigation Property
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; } // Navigation Property

        public Order()
        {
            Details = new List<OrderDetail>();
            Status = OrderStatus.Ordered;
        }

    }
}
