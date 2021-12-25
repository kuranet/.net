using AutoMapper;
using BuisnessLogic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private Mapper _mapper;

        private Mapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Menu, MenuView>());
                    _mapper = new Mapper(configuration);
                }
                return _mapper;
            }
        }

        [HttpGet]
        public List<MenuView> Get()
        {
            var order = Mapper.Map<List<Menu>, List<MenuView>>(MenuController.Instance.Menus.ToList());
            return order;
        }

        [HttpGet("{id}")]
        public MenuView Get(int id)
        {
            var meal = MenuController.Instance.Menus.FirstOrDefault((p) => p.Id == id);
            var view = Mapper.Map<MenuView>(meal);
            return view;
        }

        [HttpPost]
        public ActionResult<MenuView> Post(Menu mealToAdd)
        {
            if (mealToAdd == null)
            {
                return BadRequest();
            }

            MenuController.Instance.AddMenu(mealToAdd.Name);
            var view = Mapper.Map<Menu, MenuView>(mealToAdd);
            return Ok(view);
        }

        [HttpDelete("{id}")]
        public ActionResult<Menu> DeleteProducts(int id)
        {
            var mealToDelete = MenuController.Instance.Menus.FirstOrDefault((p) => p.Id == id);
            if (mealToDelete == null)
            {
                return NotFound();
            }
            else
            {
                MenuController.Instance.RemoveMenu(mealToDelete);
                return Ok(mealToDelete);
            }
        }

        [HttpGet]
        [Route("GetInMenuMeals")]
        public List<Meal> GetInMenuIngredients(int id)
        {
            var meal = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == id);
            return meal?.Meals;
        }

        [HttpGet]
        [Route("GetAddableMenuMeals")]
        public List<Meal> GetAddableMenuIngredients(int id)
        {
            var editingMeal = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == id);

            var allIngredients = MealController.Instance.Meals;
            var addable = allIngredients
                .Where(meal => MenuController.Instance.CanAddMealToMenu(editingMeal, meal))
                .ToList();

            return addable;
        }

        [HttpPut]
        [Route("AddMeal")]
        public ActionResult AddMealToMenu(int menuId, [FromBody] Meal meal)
        {
            var menu = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == menuId);
            var menuAvailable = MenuController.Instance.Menus.Any(m => m.Id == menu.Id);
            if (menuAvailable == false)
                return BadRequest();

            var mealAvailable = MealController.Instance.CanOperateMeal(meal);
            if (mealAvailable == false)
                return BadRequest();

            MenuController.Instance.AddMealToMenu(menu, meal);

            return Ok();
        }

        [HttpPut]
        [Route("RemoveMeal")]
        public ActionResult RemoveMealFromMenu(int menuId, [FromBody] Meal meal)
        {
            var menu = MenuController.Instance.Menus.FirstOrDefault(m => m.Id == menuId);
            var menuAvailable = MenuController.Instance.Menus.Any(m => m.Id == menu.Id);
            if (menuAvailable == false)
                return BadRequest();

            var mealAvailable = MealController.Instance.CanOperateMeal(meal);
            if (mealAvailable == false)
                return BadRequest();

            MenuController.Instance.RemoveMealFromMenu(menu, meal);

            return Ok();
        }
    }
}
