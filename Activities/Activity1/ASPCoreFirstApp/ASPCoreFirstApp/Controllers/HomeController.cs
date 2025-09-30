using ASPCoreFirstApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPCoreFirstApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "This is going to be a great day";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is an about page";
            return View("AboutMe");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "This is a contact page";
            return View("ContactMe");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
