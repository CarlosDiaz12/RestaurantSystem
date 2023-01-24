using RestaurantSystem.Shared.OrderInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.Report
{
    public class GenerateInvoiceDTO
    {
        public int OrderId { get; set; }
        public string ImagesPath { get; set; }
        public InvoiceDto InvoiceData { get; set; }

    }
}
