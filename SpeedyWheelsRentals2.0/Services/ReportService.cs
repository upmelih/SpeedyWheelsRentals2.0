using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SpeedyWheelsRentals.Models;
using System.Collections.Generic;
using System.IO;



namespace SpeedyWheelsRentals2._0.Services
{


    public class ReportService
    {

        public string GeneratePdfReportToFile(List<Reservation> reservations)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".pdf");
            using (var writer = new PdfWriter(tempFilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    document.Add(new Paragraph("Reservations Report").SetTextAlignment(TextAlignment.CENTER));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 3, 3, 3, 3 })).UseAllAvailableWidth();
                    table.AddHeaderCell("Start Date");
                    table.AddHeaderCell("End Date");
                    table.AddHeaderCell("Status");
                    table.AddHeaderCell("Customer");
                    table.AddHeaderCell("Vehicle");
                    table.AddHeaderCell("Cost");

                    foreach (var reservation in reservations)
                    {
                        table.AddCell(reservation.StartDate.ToString());
                        table.AddCell(reservation.EndDate.ToString());
                        table.AddCell(reservation.Status.ToString());
                        table.AddCell(reservation.Customer?.Name ?? "N/A");
                        table.AddCell(reservation.Vehicle?.Make ?? "N/A");
                        table.AddCell(reservation.ReservationCost.ToString("C"));
                    }

                    document.Add(table);
                }
            }
            return tempFilePath; // Return the path to the temporary file
        }




    }
}
