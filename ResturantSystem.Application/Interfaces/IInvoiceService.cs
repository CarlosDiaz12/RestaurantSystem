using RestaurantSystem.Shared.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<byte[]> GenerateInvoiceReport(GenerateInvoiceDTO dto);
    }
}
