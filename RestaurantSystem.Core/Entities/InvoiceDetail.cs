using RestaurantSystem.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class InvoiceDetail: BaseEntity
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
