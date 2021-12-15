using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLogic;
using DataLayer;

namespace BusinessLogic
{
    public class RootController
    {
        private static RootController instance;

        private RootController()
        { }

        public static RootController Instance
        {
            get
            {
                if (instance == null)
                    instance = new RootController();
                return instance;
            }
        }

        private PostITEntities _concreteDatabase;
        private IDataProvider DataProvider => _concreteDatabase;

        public void Initialize()
        {
            _concreteDatabase = new PostITEntities();

            MenuController.Instance.Initialize(DataProvider);
            MealController.Instance.Initialize(DataProvider);
            IngredientController.Instance.Initialize(DataProvider);
        }

        public void Quit()
        {
            _concreteDatabase.SaveChanges();
        }
    }
}
