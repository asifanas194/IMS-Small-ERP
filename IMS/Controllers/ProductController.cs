using IMS.Filters;
using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IMS.Controllers
{
    [SessionAuthorize]
    public class ProductController : Controller
    {
        private readonly ImsContext _context;

        public ProductController(ImsContext context)
        {
            _context = context;
        }

        // ================== Index ==================
        public async Task<IActionResult> Index(int? categoryId, int? vendorId)
        {
            var products = await (from p in _context.Products
                                  join c in _context.Categories on p.CategoryId equals c.CategoryId
                                  join v in _context.Vendors on p.VendorId equals v.VendorId
                                  where (!categoryId.HasValue || p.CategoryId == categoryId)
                                     && (!vendorId.HasValue || p.VendorId == vendorId)
                                  select new
                                  {
                                      p.ProductId,
                                      p.ProductName,
                                      CategoryName = c.CategoryName,
                                      VendorName = v.VendorName,
                                      p.Quantity,
                                      p.CostPrice,
                                      p.SalePrice
                                  }).ToListAsync();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Vendors = new SelectList(await _context.Vendors.ToListAsync(), "VendorId", "VendorName");

            return View(products);
        }


        // ================== Create ==================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Vendors = new SelectList(await _context.Vendors.ToListAsync(), "VendorId", "VendorName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (product.Quantity <= 0)
                ModelState.AddModelError("Quantity", "Quantity must be greater than zero.");

            if (product.CostPrice <= 0 || product.SalePrice <= 0)
                ModelState.AddModelError("CostPrice", "Price must be greater than zero.");

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Vendors = new SelectList(await _context.Vendors.ToListAsync(), "VendorId", "VendorName", product.VendorId);
            return View(product);
        }

        // ================== Edit ==================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Vendors = new SelectList(await _context.Vendors.ToListAsync(), "VendorId", "VendorName", product.VendorId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (product.Quantity <= 0)
                ModelState.AddModelError("Quantity", "Quantity must be greater than zero.");

            if (product.CostPrice <= 0 || product.SalePrice <= 0)
                ModelState.AddModelError("CostPrice", "Price must be greater than zero.");

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Vendors = new SelectList(await _context.Vendors.ToListAsync(), "VendorId", "VendorName", product.VendorId);
            return View(product);
        }

        // ================== Delete ==================
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.CategoryId
                                 join v in _context.Vendors on p.VendorId equals v.VendorId
                                 where p.ProductId == id
                                 select new
                                 {
                                     p.ProductId,
                                     p.ProductName,
                                     c.CategoryName,
                                     v.VendorName
                                 }).FirstOrDefaultAsync();

            if (product == null) return NotFound();

            ViewBag.CategoryName = product.CategoryName;
            ViewBag.VendorName = product.VendorName;

            return View(new Product { ProductId = product.ProductId, ProductName = product.ProductName });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
