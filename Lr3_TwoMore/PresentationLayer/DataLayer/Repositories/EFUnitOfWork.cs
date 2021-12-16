using System;
namespace DataLayer
{
    public class EFUnitOfWork : IDataProvider, IDisposable
    {
        private PostITEntities db;
        private MenuRepository phoneRepository;
        private MealRepository orderRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new PostITEntities(connectionString);
        }
        public IRepository<Menu> Menus
        {
            get
            {
                if (phoneRepository == null)
                    phoneRepository = new MenuRepository(db);
                return phoneRepository;
            }
        }

        public IRepository<Meal> Meals
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new MealRepository(db);
                return orderRepository;
            }
        }

        public IRepository<Ingredient> Ingredients
        {
            get
            {
                return null;
                //if (orderRepository == null)
                //    orderRepository = new MealRepository(db);
                //return orderRepository;
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
