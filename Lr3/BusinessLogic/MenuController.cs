using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class MenuController
    {
        private readonly List<Menu> _menus = new List<Menu>();
        public IReadOnlyList<Menu> Menus => _menus;

        public void AddMenu()
        {

        }
        public void RemoveMenu()
        {

        }
    }
}
