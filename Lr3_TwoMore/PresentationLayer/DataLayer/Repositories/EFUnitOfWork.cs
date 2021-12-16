using System;
namespace DataLayer
{
    public class EFUnitOfWork : IDataProvider, IDisposable
    {
        private PostITEntities db;
        private MenuRepository menuRepository;
        private MealRepository mealRepository;
        private IngredientRepository ingredientRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new PostITEntities(connectionString);
        }
        public IRepository<Menu> Menus
        {
            get
            {
                if (menuRepository == null)
                    menuRepository = new MenuRepository(db);
                return menuRepository;
            }
        }

        public IRepository<Meal> Meals
        {
            get
            {
                if (mealRepository == null)
                    mealRepository = new MealRepository(db);
                return mealRepository;
            }
        }

        public IRepository<Ingredient> Ingredients
        {
            get
            {
                if (ingredientRepository == null)
                    ingredientRepository = new IngredientRepository(db);
                return ingredientRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
