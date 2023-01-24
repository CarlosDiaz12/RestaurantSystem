using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Shared.DTOs.Report;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IWebHostEnvironment _env;
        public InvoiceController(IInvoiceService invoiceService, IWebHostEnvironment env)
        {
            _invoiceService = invoiceService;   
            _env = env;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(int orderId)
        {
            var file = await _invoiceService.GenerateInvoiceReport(new GenerateInvoiceDTO() { OrderId = orderId, ImagesPath = _env.WebRootPath});
            return File(file, "application/pdf");
        }
    }
}
