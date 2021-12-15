using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataProvider
    {
        List<Meal> Meals { get; set; }
        List<Ingredient> Ingredients { get; set; }
        List<Menu> Menus { get; set; }
    }
}
