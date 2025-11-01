using IMS.Filters;
using Microsoft.AspNetCore.Mvc;
using IMS.Models;
using IMS.Helpers;

namespace IMS.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        private readonly ImsContext _context;
        private readonly LogHelper _logHelper;

        public HomeController(ImsContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _logHelper = new LogHelper(context, accessor);
        }

        public IActionResult Index()
        {
            _logHelper.LogAction("Index", "Home");

            var role = HttpContext.Session.GetString("UserRole");

            var totalProducts = _context.Products.Count();
            var lowStock = _context.Products.Count(p => (p.Quantity ?? 0) < 10);

            ViewBag.TotalProducts = totalProducts;
            ViewBag.LowStock = lowStock;

            if (role == "Admin")
            {
                var totalVendors = _context.Vendors.Count();
                var totalValue = _context.Products
                    .Where(p => p.CostPrice != null && p.Quantity != null)
                    .Sum(p => (p.CostPrice ?? 0) * (p.Quantity ?? 0));

                ViewBag.TotalVendors = totalVendors;
                ViewBag.TotalValue = totalValue;
            }

            return View();
        }
    }
}
