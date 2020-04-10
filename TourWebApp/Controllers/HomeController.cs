using System.Diagnostics;
using System.Threading.Tasks;
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

            return View();
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
