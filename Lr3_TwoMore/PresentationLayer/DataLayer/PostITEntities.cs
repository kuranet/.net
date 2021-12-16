using System.Data.Entity;

namespace DataLayer
{
    public class PostITEntities : DbContext, IDataProvider
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        IList<Menu> IDataProvider.Menus => Menus.ToList();
        IList<Meal> IDataProvider.Meals => Meals.ToList();
        IList<Ingredient> IDataProvider.Ingredients => Ingredients.ToList();

        public PostITEntities()
        {
            Database.SetInitializer(new EntityDbInitializer());
        }
    }
}
