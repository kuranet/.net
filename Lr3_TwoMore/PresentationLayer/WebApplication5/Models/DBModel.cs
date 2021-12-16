using DataLayer;
using System.Data.Entity;

namespace WebApplication5.Models
{
    public class DBModel : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
    }
}
