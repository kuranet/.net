using AutoMapper;
using BuisnessLogic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        [HttpGet]
        public List<MealView> Get()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Meal, MealView>());
            var mapper = new Mapper(configuration);
            var order = mapper.Map<List<Meal>, List<MealView>>(MealController.Instance.Meals.ToList());
            return order;
        }

        [HttpGet("{id}")]
        public Meal Get(int id)
        {
            var meal = MealController.Instance.Meals.FirstOrDefault((p) => p.Id == id);
            return meal;
        }

        [HttpPost]
        public ActionResult<Meal> Post(Meal mealToAdd)
        {
            if (mealToAdd == null)
            {
                return BadRequest();
            }

            MealController.Instance.AddMeal(mealToAdd.Name);
            return Ok(mealToAdd);
        }

        [HttpDelete("{id}")]
        public ActionResult<Meal> DeleteProducts(int id)
        {
            var mealToDelete = MealController.Instance.Meals.FirstOrDefault((p) => p.Id == id);
            if (mealToDelete == null)
            {
                return NotFound();
            }
            else
            {
                MealController.Instance.RemoveMeal(mealToDelete);
                return Ok(mealToDelete);
            }
        }

        [HttpGet]
        [Route("GetInMenuIngredients")]
        public List<Ingredient> GetInMenuIngredients(int id)
        {
            var meal = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);
            return meal?.Ingredients;
        }

        [HttpGet]
        [Route("GetAddableMenuIngredients")]
        public List<Ingredient> GetAddableMenuIngredients(int id)
        {
            var editingMeal = MealController.Instance.Meals.FirstOrDefault(m => m.Id == id);

            var allIngredients = IngredientController.Instance.Ingredients;
            var addable = allIngredients
                .Where(meal => MealController.Instance.CanAddIngredientToMeal(editingMeal, meal))
                .ToList();

            return addable;
        }

        [HttpPut]
        [Route("AddIngredient")]
        public ActionResult AddIngredientToMeal(int mealId, [FromBody]Ingredient ingredient)
        {
            var meal = MealController.Instance.Meals.FirstOrDefault(m => m.Id == mealId);
            var menuAvailable = MealController.Instance.CanOperateMeal(meal);
            if (menuAvailable == false)
                return BadRequest();

            var mealAvailable = IngredientController.Instance.CanOperateIngredient(ingredient);
            if (mealAvailable == false)
                return BadRequest();

            MealController.Instance.AddIngredientToMeal(meal, ingredient);

            return Ok();
        }

        [HttpPut]
        [Route("RemoveIngredient")]
        public ActionResult RemoveIngredientFromMeal(int mealId, [FromBody] Ingredient ingredient)
        {
            var meal = MealController.Instance.Meals.FirstOrDefault(m => m.Id == mealId);
            var menuAvailable = MealController.Instance.CanOperateMeal(meal);
            if (menuAvailable == false)
                return BadRequest();

            var mealAvailable = IngredientController.Instance.CanOperateIngredient(ingredient);
            if (mealAvailable == false)
                return BadRequest();

            MealController.Instance.RemoveIngredientFromMeal(meal, ingredient);

            return Ok();
        }
    }
}
