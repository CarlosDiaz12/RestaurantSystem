using RestaurantSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.OrderInvoice
{
    public class InvoiceDto
    {
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public string ClientName { get; set; }
        public string PaymentMethod { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double Tip { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public int OrderId { get; set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; }
    }
}
