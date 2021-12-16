using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private PostITEntities db;

        public IngredientRepository(PostITEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return db.Ingredients;
        }

        public Ingredient Get(int id)
        {
            return db.Ingredients.Find(id);
        }

        public void Create(Ingredient book)
        {
            db.Ingredients.Add(book);
            db.SaveChanges();
        }

        public void Update(Ingredient book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Ingredient> Find(Func<Ingredient, Boolean> predicate)
        {
            return db.Ingredients.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Ingredient book = db.Ingredients.Find(id);
            if (book != null)
            {
                db.Ingredients.Remove(book);
                db.SaveChanges();
            }
        }
    }
}
