using Microsoft.AspNetCore.Mvc;

namespace ASPCoreFirstApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }

        public IActionResult Details(string Name, int Personality = 9)
        {
            ViewData["Name"] = Name;
            ViewData["Personality"] = Personality;
            return View("Message");
        }

        public IActionResult Data(int OrderNumber, float Price, int qty)
        {
            return Json(new {OrderNumber = OrderNumber, Price = Price, Quantity = qty});
        }
    }
}
