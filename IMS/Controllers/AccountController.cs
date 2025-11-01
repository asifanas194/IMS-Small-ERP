using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using IMS.Helpers;

namespace IMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ImsContext _context;
        private readonly LogHelper _logHelper;

        public AccountController(ImsContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _logHelper = new LogHelper(context, accessor);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.UserPassword == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserRole", user.UserRole);

                // ✅ Log this action
                _logHelper.LogAction("Login", "Account");

                TempData["Success"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Invalid username or password!";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string username, string password, string role)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                TempData["Error"] = "Username already exists!";
                return View();
            }

            var newUser = new User
            {
                Username = username,
                UserPassword = password,
                UserRole = role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            _logHelper.LogAction("Register", "Account");

            TempData["Success"] = "Registration successful! You can now log in.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _logHelper.LogAction("Logout", "Account");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
