using RestaurantSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.OrderInvoice
{
    public class InvoiceDetailDto
    {
        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
