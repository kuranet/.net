using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    public class IngredientController
    {
        private static IngredientController instance;

        private IngredientController()
        { }

        public static IngredientController Instance
        {
            get
            {
                if (instance == null)
                    instance = new IngredientController();
                return instance;
            }
        }

        public IList<Ingredient> Ingredients => DataProvider.Ingredients;

        private IDataProvider DataProvider { get; set; }

        public void Initialize(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public bool CanAddIngr(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var menuWithSuchName = Ingredients.FirstOrDefault(meal => meal.Name == name);
            if (menuWithSuchName != null)
                return false;

            return true;
        }

        public void AddIngredient(string name)
        {
            var hasSuchName = Ingredients.Any(x => x.Name == name);
            if (hasSuchName)
                throw new Exception("Such ingredient also exists");

            var newIngr = new Ingredient()
            {
                Name = name,
            };

            Ingredients.Add(newIngr);
        }

        public void RemoveIngredient(Ingredient ingr)
        {
            if (CanOperateIngredient(ingr) == false)
                throw new Exception($"Unknown ingredient {ingr.Name}");

            Ingredients.Remove(ingr);
        }

        public bool CanOperateIngredient(Ingredient ingredient)
            => Ingredients.Contains(ingredient);
    }
}
