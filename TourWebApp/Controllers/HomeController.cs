using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleHashing;
using TourWebApp.Data;
using TourWebApp.Models;

namespace TourWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TourContext _context;

        public HomeController(TourContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int loginID, string password)
        {
            var login = await _context.Logins.FindAsync(loginID);
            if (login == null || !PBKDF2.Verify(login.PasswordHash, password)) 
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { LoginID = loginID });
            }

            var user = _context.Users.Where(e => e.Login.LoginID == loginID).Single();
            HttpContext.Session.SetInt32(nameof(Models.User.UserID), Convert.ToInt32(user.UserID));
            HttpContext.Session.SetString(nameof(Models.User.Role), user.Role);
            return RedirectToAction("Index", "Locations");
        }

        [Route("LogoutNow")]
        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
