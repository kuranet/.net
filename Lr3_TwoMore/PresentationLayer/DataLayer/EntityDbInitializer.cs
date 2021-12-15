using System.Data.Entity;

namespace DataLayer
{
    public class EntityDbInitializer : DropCreateDatabaseAlways<PostITEntities>
    {
        protected override void Seed(PostITEntities db)
        {
            var tomato = new Ingredient() { Name = "Tomato" };
            var cheese = new Ingredient() { Name = "Cheese" };
            var pizzaBase = new Ingredient() { Name = "Base for pizza" };
            var apple = new Ingredient() { Name = "Apple" };
            var pasta = new Ingredient() { Name = "Pasta" };
            var meatBalls = new Ingredient() { Name = "MeatBalls" };
            var dough = new Ingredient() { Name = "Dough" };
            var bouillon = new Ingredient() { Name = "Bouillon" };
            db.Ingredients.Add(cheese);
            db.Ingredients.Add(tomato);
            db.Ingredients.Add(pizzaBase);
            db.Ingredients.Add(apple);
            db.Ingredients.Add(pasta);
            db.Ingredients.Add(meatBalls);
            db.Ingredients.Add(dough);
            db.Ingredients.Add(bouillon);

            var pizza = new Meal { Name = "Pizza", Ingredients = { tomato, cheese, pizzaBase }, Price = 100 };
            var appleJuice = new Meal() { Name = "Apple juice", Ingredients = { apple }, Price = 15 };
            var pastaWithMeatBalls = new Meal() { Name = "Pasta with meatballs", Ingredients = { pasta, meatBalls }, Price = 86 };
            var pelmeni = new Meal() { Name = "Pelmeni", Ingredients = { meatBalls, dough }, Price = 90 };
            var borsch = new Meal() { Name = "Borsch", Ingredients = { meatBalls, bouillon }, Price = 110 };
            db.Meals.Add(pizza);
            db.Meals.Add(appleJuice);
            db.Meals.Add(pastaWithMeatBalls);
            db.Meals.Add(pelmeni);
            db.Meals.Add(borsch);

            var italianoMenun = new Menu() { Name = "Italiano", Meals = { pizza, appleJuice, pastaWithMeatBalls } };
            var traditionalMenun = new Menu() { Name = "Traditional", Meals = { pelmeni, borsch } };
            db.Menus.Add(italianoMenun);
            db.Menus.Add(traditionalMenun);

            base.Seed(db);
        }
    }
}
