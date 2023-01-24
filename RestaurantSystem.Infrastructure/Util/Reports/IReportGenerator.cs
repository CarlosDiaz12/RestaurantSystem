using RestaurantSystem.Shared.DTOs.Report;

namespace RestaurantSystem.Infrastructure.Util.Reports
{
    public interface IReportGenerator
    {
        byte[] GenerateInvoiceReport(GenerateInvoiceDTO dto);
    }
}
