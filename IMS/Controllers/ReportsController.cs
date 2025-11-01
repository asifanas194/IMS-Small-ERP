using ClosedXML.Excel;
using IMS.Filters;
using IMS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using Document = iTextSharp.text.Document;

namespace IMS.Controllers
{
    [SessionAuthorize]
    public class ReportsController : Controller
    {
        private readonly ImsContext _context;

        public ReportsController(ImsContext context)
        {
            _context = context;
        }

        // 🔹 All Products Report
        public IActionResult AllProducts()
        {
            var products = (from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.CategoryId into catJoin
                            from c in catJoin.DefaultIfEmpty()
                            join v in _context.Vendors on p.VendorId equals v.VendorId into venJoin
                            from v in venJoin.DefaultIfEmpty()
                            select new
                            {
                                p.ProductName,
                                Category = c != null ? c.CategoryName : "N/A",
                                Vendor = v != null ? v.VendorName : "N/A",
                                p.Quantity,
                                p.CostPrice,
                                p.SalePrice
                            }).ToList();

            return View(products);
        }

        // 🔹 Low Stock Report (Quantity < 10)
        public IActionResult LowStock()
        {
            var products = (from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.CategoryId into catJoin
                            from c in catJoin.DefaultIfEmpty()
                            where p.Quantity < 10
                            select new
                            {
                                p.ProductName,
                                Category = c != null ? c.CategoryName : "N/A",
                                p.Quantity
                            }).ToList();

            return View(products);
        }

        // 🔹 Vendor List
        public IActionResult VendorList()
        {
            var vendors = _context.Vendors.ToList();
            return View(vendors);
        }

        // 🔹 Export to Excel (All Products)
        public IActionResult ExportToExcel()
        {
            var products = (from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.CategoryId into catJoin
                            from c in catJoin.DefaultIfEmpty()
                            join v in _context.Vendors on p.VendorId equals v.VendorId into venJoin
                            from v in venJoin.DefaultIfEmpty()
                            select new
                            {
                                p.ProductName,
                                Category = c != null ? c.CategoryName : "N/A",
                                Vendor = v != null ? v.VendorName : "N/A",
                                p.Quantity,
                                p.CostPrice,
                                p.SalePrice
                            }).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("All Products");
                worksheet.Cell(1, 1).Value = "Product Name";
                worksheet.Cell(1, 2).Value = "Category";
                worksheet.Cell(1, 3).Value = "Vendor";
                worksheet.Cell(1, 4).Value = "Quantity";
                worksheet.Cell(1, 5).Value = "Cost Price";
                worksheet.Cell(1, 6).Value = "Sale Price";

                int row = 2;
                foreach (var p in products)
                {
                    worksheet.Cell(row, 1).Value = p.ProductName;
                    worksheet.Cell(row, 2).Value = p.Category;
                    worksheet.Cell(row, 3).Value = p.Vendor;
                    worksheet.Cell(row, 4).Value = p.Quantity;
                    worksheet.Cell(row, 5).Value = p.CostPrice;
                    worksheet.Cell(row, 6).Value = p.SalePrice;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "AllProducts.xlsx");
                }
            }
        }

        // 🔹 Export to PDF (All Products)
        public IActionResult ExportToPdf()
        {
            var products = (from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.CategoryId into catJoin
                            from c in catJoin.DefaultIfEmpty()
                            join v in _context.Vendors on p.VendorId equals v.VendorId into venJoin
                            from v in venJoin.DefaultIfEmpty()
                            select new
                            {
                                p.ProductName,
                                Category = c != null ? c.CategoryName : "N/A",
                                Vendor = v != null ? v.VendorName : "N/A",
                                p.Quantity,
                                p.CostPrice,
                                p.SalePrice
                            }).ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph title = new Paragraph("All Products Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 100;

                table.AddCell("Product Name");
                table.AddCell("Category");
                table.AddCell("Vendor");
                table.AddCell("Quantity");
                table.AddCell("Cost Price");
                table.AddCell("Sale Price");

                foreach (var p in products)
                {
                    table.AddCell(p.ProductName ?? "");
                    table.AddCell(p.Category ?? "");
                    table.AddCell(p.Vendor ?? "");
                    table.AddCell(p.Quantity?.ToString() ?? "");
                    table.AddCell(p.CostPrice?.ToString() ?? "");
                    table.AddCell(p.SalePrice?.ToString() ?? "");
                }

                doc.Add(table);
                doc.Close();

                return File(ms.ToArray(), "application/pdf", "AllProducts.pdf");
            }
        }
    }
}
