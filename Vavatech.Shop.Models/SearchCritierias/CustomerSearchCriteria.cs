using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shop.Models.SearchCritierias
{
    public abstract class SearchCriteria : Base
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
