using RestaurantSystem.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class InvoiceCharge
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int ChargeId { get; set; }
        public Charge Charge { get; set; }
    }
}
