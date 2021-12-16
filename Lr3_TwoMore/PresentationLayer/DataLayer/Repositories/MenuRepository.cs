using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class MenuRepository : IRepository<Menu>
    {
        private PostITEntities db;

        public MenuRepository(PostITEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Menu> GetAll()
        {
            return db.Menus.Include(o => o.Meals);
        }

        public Menu Get(int id)
        {
            return db.Menus.Find(id);
        }

        public void Create(Menu book)
        {
            db.Menus.Add(book);
            db.SaveChanges();
        }

        public void Update(Menu book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Menu> Find(Func<Menu, Boolean> predicate)
        {
            return db.Menus.Include(o => o.Meals).Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Menu book = db.Menus.Find(id);
            if (book != null)
            {
                db.Menus.Remove(book);
                db.SaveChanges();
            }
        }
    }
}
