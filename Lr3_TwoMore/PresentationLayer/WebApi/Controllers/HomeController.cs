using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult GoToOrderEditing()
        {
            return Redirect("https://localhost:7104/orders.html");
        }

        [HttpGet]
        public ActionResult GoToMenuEditing()
        {
            return Redirect("https://localhost:7104/menus.html");
        }

        [HttpGet]
        public ActionResult GoToMealEditing()
        {
            return Redirect("https://localhost:7104/meals.html");
        }
    }
}