// using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace DataLayer
{
    public class PostITEntities : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public PostITEntities()
        {
            Database.SetInitializer(new EntityDbInitializer());
        }

        public PostITEntities(string connectionString)
            : base()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
