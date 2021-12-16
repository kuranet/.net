using BuisnessLogic;
using BusinessLogic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    public class MenuEditingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Menus = MenuController.Instance.Menus;
            return View();
        }

        [HttpGet]
        public void Remove(int id)
        {
            var menuToDelete = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == id);
            MenuController.Instance.RemoveMenu(menuToDelete);

            // return Redirect("/MenuEditing/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var menuToEdit = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == id);
            ViewBag.MealsInMenu = menuToEdit.Meals;

            var allMeals = MealController.Instance.Meals;
            ViewBag.MealsCanBeAddedToMenu = allMeals
                .Where(meal => MenuController.Instance.CanAddMealToMenu(menuToEdit, meal))
                .ToList();

            return View();
        }

        [HttpPost]
        public string AddMeals(string[] countries)
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }

        [HttpPost]
        public string RemoveMeals(string[] countries)
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }
    }
}
