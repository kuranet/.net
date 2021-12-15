using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
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

        [HttpGet]
        public ActionResult GoToOrderEditing()
        {
            return Redirect("/OrderEditing/Index");
        }

        [HttpPost]
        public void GoToMenuEditing()
        {

        }
        [HttpPost]
        public void GoToMealEditing()
        {

        }
    }
}