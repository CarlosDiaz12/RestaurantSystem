using iTextSharp.text;
using iTextSharp.text.pdf;
using RestaurantSystem.Shared.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Infrastructure.Util.Reports
{
    public class PDFReportGenerator : IReportGenerator
    {
        public byte[] GenerateInvoiceReport(GenerateInvoiceDTO dto)
        {
            Document document = new Document(PageSize.Letter);
            MemoryStream ms = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);

            document.AddAuthor("BocadoSystem");
            document.AddTitle($"Invoice {dto.InvoiceData.InvoiceNumber}");
            document.Open();

            Image banner = Image.GetInstance(Path.Combine(dto.ImagesPath, "banner.jpg"));
            banner.ScaleAbsoluteHeight(140);
            banner.ScaleAbsoluteWidth(620);
            banner.SetAbsolutePosition(0, document.GetTop(100));

            document.Add(banner);

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font titleFont = new Font(baseFont, 28f, Font.BOLD, BaseColor.White);

            var titulo = new Paragraph();
            titulo.Add(new Chunk("\n"));
            titulo.Add(new Chunk("\n"));
            titulo.Add(new Paragraph("INVOICE", titleFont));

            titulo.Alignment = Element.ALIGN_CENTER;
            document.Add(titulo);

            PdfPTable table = new PdfPTable(2);
            table.SpacingBefore = 60;
            table.WidthPercentage = 95;
            
            Font invoiceInfoFont = new Font(baseFont, 10f, Font.BOLD);
            var invoiceInfo = new Paragraph($"INVOICE TO: {dto.InvoiceData.ClientName.ToUpper()}", invoiceInfoFont);


            var invoiceNumber = new Paragraph($"INVOICE #: {dto.InvoiceData.InvoiceNumber}", invoiceInfoFont);

            table.AddCell(new PdfPCell(invoiceInfo) { Border = 0});
            table.AddCell(new PdfPCell(invoiceNumber) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT});

            table.AddCell(new PdfPCell() { Border = 0});
            var invoiceDate = new Paragraph($"DATE: {dto.InvoiceData.Date:dd/MM/yyyy}", invoiceInfoFont);

            table.AddCell(new PdfPCell(invoiceDate) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT});
            document.Add(table);

            PdfPTable detailsTable = new PdfPTable(new float[] { 5f, 50f, 20f, 20f, 20f});
            detailsTable.SpacingBefore = 35;
            detailsTable.WidthPercentage = 100;

            Font invoiceDetailInfoFont = new Font(baseFont, 10f, Font.BOLD);
            detailsTable.AddCell(new Paragraph("#", invoiceDetailInfoFont));
            detailsTable.AddCell(new Paragraph("ITEM DESCRIPTION", invoiceDetailInfoFont));
            detailsTable.AddCell(new Paragraph("PRICE", invoiceDetailInfoFont));
            detailsTable.AddCell(new Paragraph("QTY", invoiceDetailInfoFont));
            detailsTable.AddCell(new Paragraph("TOTAL", invoiceDetailInfoFont));

            int index = 1;
            foreach (var detalle in dto.InvoiceData.InvoiceDetails)
            {
                Font invoiceItemDetailFont = new Font(baseFont, 9f, Font.NORMAL);
                detailsTable.AddCell(new Paragraph(index.ToString(), invoiceItemDetailFont));
                detailsTable.AddCell(new Paragraph(detalle.MenuItem.Description, invoiceItemDetailFont));
                detailsTable.AddCell(new Paragraph(detalle.UnitPrice.ToString("C"), invoiceItemDetailFont));
                detailsTable.AddCell(new Paragraph(detalle.Quantity.ToString(), invoiceItemDetailFont));
                detailsTable.AddCell(new Paragraph(detalle.Total.ToString("C"), invoiceItemDetailFont));

                index++;
            }
            document.Add(detailsTable);

            PdfPTable summaryTable = new PdfPTable(2);
            summaryTable.SpacingBefore = 10;
            summaryTable.WidthPercentage = 100;

            Font invoiceSummaryFont = new Font(baseFont, 9f, Font.BOLD);

            summaryTable.AddCell(new PdfPCell() { Border = 0 });
            summaryTable.AddCell(new PdfPCell(new Paragraph($"SUB-TOTAL:       {dto.InvoiceData.SubTotal.ToString("C")}", invoiceSummaryFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

            summaryTable.AddCell(new PdfPCell() { Border = 0 });
            summaryTable.AddCell(new PdfPCell(new Paragraph($"TIP:       {dto.InvoiceData.Tip.ToString("C")}", invoiceSummaryFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

            summaryTable.AddCell(new PdfPCell() { Border = 0 });
            summaryTable.AddCell(new PdfPCell(new Paragraph($"TAX:       {dto.InvoiceData.Tax.ToString("C")}", invoiceSummaryFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

            summaryTable.AddCell(new PdfPCell() { Border = 0 });
            summaryTable.AddCell(new PdfPCell() { Border = 0 });

            summaryTable.AddCell(new PdfPCell() { Border = 0 });
            summaryTable.AddCell(new PdfPCell(new Paragraph($"TOTAL:       {dto.InvoiceData.Total.ToString("C")}", invoiceSummaryFont)) { Border = 1, HorizontalAlignment = Element.ALIGN_RIGHT });

            document.Add(summaryTable);

            pdfWriter.Close();
            return ms.ToArray();
        }
    }
}
