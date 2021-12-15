using BuisnessLogic;
using DataLayer;

namespace PresentationLayer.Commands
{
    public class OperateMenuCommand : Command
    {
        public override void Execute()
        {
            Console.WriteLine("Editor menu:\n" +
                "1. See all menus\n" +
                "2. Edit menu\n" +
                "3. Add menu\n" +
                "4. Remove menu");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        for (int i = 0; i < MenuController.Instance.Menus.Count; i++)
                        {
                            var menu = MenuController.Instance.Menus[i];
                            Console.WriteLine($"{i + 1} {menu.Name}");
                        }
                        break;
                    }
                case "2":
                    {
                        var selectedMenu = SelectMenu();
                        (new EditSelectedMenuCommand()).Execute(selectedMenu);
                        break;
                    }
                case "3":
                    {
                        var isNameCorrect = false;
                        var name = string.Empty;
                        while (isNameCorrect == false)
                        {
                            Console.Write("Enter men menu name: ");
                            name = Console.ReadLine();
                            isNameCorrect = MenuController.Instance.CanAddMenu(name);
                            if (isNameCorrect == false)
                                Console.WriteLine("Incorrect name");
                        }

                        MenuController.Instance.AddMenu(name);
                        Console.WriteLine("Menu successfully added");
                        break;
                    }
                case "4":
                    {
                        var selectedMenu = SelectMenu();
                        MenuController.Instance.RemoveMenu(selectedMenu);
                        break;
                    }
                case "b":
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Incorrent input");
                        break;
                    }
            }
        }

        private Menu SelectMenu()
        {
            var allMenus = MenuController.Instance.Menus;
            for (int i = 0; i < allMenus.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allMenus[i].Name}");
            }
            var isSelectedCorrectly = false;
            var menuIndex = -1;
            while (isSelectedCorrectly == false)
            {
                Console.Write("Select menu to exit: ");
                var indexStr = Console.ReadLine();
                isSelectedCorrectly = int.TryParse(indexStr, out menuIndex);
                if (isSelectedCorrectly == false)
                {
                    continue;
                }
                menuIndex--;
                isSelectedCorrectly = menuIndex >= 0 && menuIndex < allMenus.Count;
            }

            var selectedMenu = allMenus[menuIndex];
            return selectedMenu;
        }
    }
}
