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
        public ActionResult Remove(int id)
        {
            var menuToDelete = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == id);
            MenuController.Instance.RemoveMenu(menuToDelete);

            return Redirect("/MenuEditing/Index");
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {
            var menuToEdit = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == id);
            ViewBag.MenuName = menuToEdit.Name;
            ViewBag.menuId = menuToEdit.Id;
            ViewBag.InMenuMeals = menuToEdit.Meals;

            var allMeals = MealController.Instance.Meals;
            ViewBag.MealsCanBeAddedToMenu = allMeals
                .Where(meal => MenuController.Instance.CanAddMealToMenu(menuToEdit, meal))
                .ToList();

            return View();
        }

        [HttpGet]
        [Route("MenuEditing/AddMeal/{menuId}&{id}")]
        public ActionResult AddMeal(int menuId, int id)
        {
            var menu = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == menuId);
            var mealToAdd = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);

            MenuController.Instance.AddMealToMenu(menu, mealToAdd);
            
            return Redirect($"/MenuEditing/Edit/{menuId}");
        }

        [HttpGet]
        [Route("MenuEditing/RemoveMeal/{menuId}&{id}")]
        public ActionResult RemoveMeal(int menuId, int id)
        {
            var menu = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == menuId);
            var mealToRemove = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);

            MenuController.Instance.RemoveMealFromMenu(menu, mealToRemove);

            return Redirect($"/MenuEditing/Edit/{menuId}");
        }
    }
}
