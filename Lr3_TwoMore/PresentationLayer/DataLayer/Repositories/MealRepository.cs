using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class MealRepository : IRepository<Meal>
    {
        private PostITEntities db;

        public MealRepository(PostITEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Meal> GetAll()
        {
            return db.Meals.Include(o => o.Ingredients);
        }

        public Meal Get(int id)
        {
            return db.Meals.Find(id);
        }

        public void Create(Meal order)
        {
            db.Meals.Add(order);
            db.SaveChanges();
        }

        public void Update(Meal order)
        {
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
        }
        public IEnumerable<Meal> Find(Func<Meal, Boolean> predicate)
        {
            return db.Meals.Include(o => o.Ingredients).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Meal order = db.Meals.Find(id);
            if (order != null)
            {
                db.Meals.Remove(order);
                db.SaveChanges();
            }
        }
    }
}
