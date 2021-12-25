using AutoMapper;
using BuisnessLogic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        [HttpGet]
        public List<IngredientView> Get()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Ingredient, IngredientView>());
            var mapper = new Mapper(configuration);
            var order = mapper.Map<List<Ingredient>, List<IngredientView>>(IngredientController.Instance.Ingredients.ToList());
            return order;
        }

        [HttpGet("{id}")]
        public Ingredient Get(int id)
        {
            var ingredient = IngredientController.Instance.Ingredients.FirstOrDefault((p) => p.Id == id);
            return ingredient;
        }

        [HttpPost]
        public ActionResult<Ingredient> Post(Ingredient ingredientToAdd)
        {
            if (ingredientToAdd == null)
            {
                return BadRequest();
            }

            IngredientController.Instance.AddIngredient(ingredientToAdd.Name);
            return Ok(ingredientToAdd);
        }

        [HttpDelete("{id}")]
        public ActionResult<Ingredient> DeleteProducts(int id)
        {
            var ingredientToDelete = IngredientController.Instance.Ingredients.FirstOrDefault((p) => p.Id == id);
            if (ingredientToDelete == null)
            {
                return NotFound();
            }
            else
            {
                IngredientController.Instance.RemoveIngredient(ingredientToDelete);
                return Ok(ingredientToDelete);
            }
        }
    }
}
