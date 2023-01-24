using AutoMapper;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Util.Reports;
using RestaurantSystem.Shared.DTOs;
using RestaurantSystem.Shared.DTOs.Report;
using RestaurantSystem.Shared.OrderInvoice;
using ResturantSystem.Application.Exceptions;
using ResturantSystem.Application.Interfaces;
using ResturantSystem.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportGenerator _reportGenerator;
        private readonly IMapper _mapper;
        public InvoiceService(
            IUnitOfWork unitOfWork, 
            IReportGenerator reportGenerator,
            IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _reportGenerator= reportGenerator;
            _mapper = mapper;
        }

        public async Task<byte[]> GenerateInvoiceReport(GenerateInvoiceDTO dto)
        {
            var invoice = await GetInvoiceInfo(orderId: dto.OrderId);
            var invoiceData = _mapper.Map<InvoiceDto>(invoice.Item1);
            if(invoice.Item2)
            {
                invoiceData.Tip = (Constants.TipPercentage * invoiceData.SubTotal);
            }
            dto.InvoiceData = invoiceData;
            return _reportGenerator.GenerateInvoiceReport(dto);
        }

        private async Task<(Invoice, bool)> GetInvoiceInfo(int orderId)
        {
            Invoice invoice;
            // si la orden tiene factura, la devuelvo, sino la creo
            var includeProps = $"{nameof(Invoice.InvoiceDetails)},{nameof(Invoice.InvoiceDetails)}.{nameof(InvoiceDetail.MenuItem)}";
            invoice = await _unitOfWork.InvoiceRepository.Get(x => x.OrderId == orderId, includeProperties: includeProps);
            var includePropsOrder = $"{nameof(Order.OrderDetails)}";
            var order = await _unitOfWork.OrderRepository.Get(x => x.Id == orderId, includeProperties: includePropsOrder);

            if (invoice != null)
                return (invoice, order.TableId.HasValue);

               invoice = _mapper.Map<Invoice>(order);

                invoice.Date= DateTime.Now;
                invoice.OrderId= orderId;
                invoice.Tax = (invoice.SubTotal * Constants.ItbisPercentage);
                var invoices = await _unitOfWork.InvoiceRepository.GetAll();
                int invoiceQty = invoices.Count() + 1;
                string invoiceNumber = invoiceQty.ToString(); 
                invoice.InvoiceNumber = invoiceNumber.PadLeft(6, '0');

                await _unitOfWork.InvoiceRepository.Insert(invoice);
                await _unitOfWork.SaveChangesAsync();
            

            return (await _unitOfWork.InvoiceRepository.Get(x => x.OrderId == orderId, includeProperties: includeProps), order.TableId.HasValue);
        }
    }
}
