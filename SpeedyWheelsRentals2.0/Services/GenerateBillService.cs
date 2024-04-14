using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SpeedyWheelsRentals.Models;
using System.Collections.Generic;
using System.IO;


namespace SpeedyWheelsRentals2._0.Services
{
    public class GenerateBillService
    {
        public string GeneratePdfBillToFile(Reservation? reservation)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".pdf");
            using (var writer = new PdfWriter(tempFilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate());
                    document.Add(new Paragraph("Billing Summury").SetTextAlignment(TextAlignment.CENTER));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 2, 2, 2, 2, 2, 2, 2 })).UseAllAvailableWidth();
                   
                    table.AddHeaderCell("Customer Name");
                    table.AddHeaderCell("Customer Email");
                    table.AddHeaderCell("Customer PhoneNumber");
                    table.AddHeaderCell("Car Model");
                    table.AddHeaderCell("Car daiyl rental cost");
                    table.AddHeaderCell("Reservation Cost before tax");
                    table.AddHeaderCell("Total Price after Tax(tax rate : 5%)");

                    

                    
                        
                        table.AddCell(reservation.Customer?.Name ?? "N/A");
                        table.AddCell(reservation.Customer?.Email ?? "N/A");
                        table.AddCell(reservation.Customer?.PhoneNumber ?? "N/A");
                        table.AddCell(reservation.Vehicle?.Model ?? "N/A");
                        table.AddCell(reservation.Vehicle?.DailyRentalPrice.ToString("C"));
                        table.AddCell(reservation.ReservationCost.ToString("C"));
                    var totalCostAfterTax = reservation.ReservationCost * 1.05;
                    table.AddCell(totalCostAfterTax.ToString("C"));
                    

                    document.Add(table);
                }
            }
            return tempFilePath; // Return the path to the temporary file
        }
    }
}
