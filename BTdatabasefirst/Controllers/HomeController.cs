using BTdatabasefirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BTdatabasefirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CshapCuoikiContext _context;

        public HomeController(ILogger<HomeController> logger, CshapCuoikiContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var nv = _context.Nhanviens.ToList();
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
