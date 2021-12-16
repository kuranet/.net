using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication5.Models;

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

        [HttpGet]
        public ActionResult GoToMenuEditing()
        {
            return Redirect("/MenuEditing/Index");
        }

        [HttpGet]
        public ActionResult GoToMealEditing()
        {
            return Redirect("/MealEditing/Index");
        }
    }
}