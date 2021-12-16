using BuisnessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    public class MealEditingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Menus = MealController.Instance.Meals;
            return View();
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var menuToDelete = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);
            MealController.Instance.RemoveMeal(menuToDelete);

            return Redirect("/MealEditing/Index");
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {
            var menuToEdit = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);
            ViewBag.MenuName = menuToEdit.Name;
            ViewBag.menuId = menuToEdit.Id;
            ViewBag.InMenuMeals = menuToEdit.Ingredients;

            var allIngredients = IngredientController.Instance.Ingredients;
            ViewBag.MealsCanBeAddedToMenu = allIngredients
                .Where(meal => MealController.Instance.CanAddIngredientToMeal(menuToEdit, meal))
                .ToList();

            return View();
        }

        [HttpGet]
        [Route("MealEditing/AddIngredient/{ingrId}&{id}")]
        public ActionResult AddIngredient(int ingrId, int id)
        {
            var meal = MealController.Instance.Meals.FirstOrDefault(m => m.Id == ingrId);
            var ingredientToAdd = IngredientController.Instance.Ingredients.FirstOrDefault(m => m.Id == id);

            MealController.Instance.AddIngredientToMeal(meal, ingredientToAdd);
            
            return Redirect($"/MealEditing/Edit/{ingrId}");
        }

        [HttpGet]
        [Route("MealEditing/RemoveIngredient/{ingrId}&{id}")]
        public ActionResult RemoveIngredient(int ingrId, int id)
        {
            var menu = MealController.Instance.Meals.FirstOrDefault(m => m.Id == ingrId);
            var mealToRemove = IngredientController.Instance.Ingredients.FirstOrDefault(m => m.Id == id);

            MealController.Instance.RemoveIngredientFromMeal(menu, mealToRemove);

            return Redirect($"/MealEditing/Edit/{ingrId}");
        }
    }
}
