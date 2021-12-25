using System.Net.Http.Json;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //MealControllerRequests();
            //MenuControllerRequests();
            OrderControllerRequests();

            Console.ReadLine();
        }

        private static void MealControllerRequests()
        {
            Console.WriteLine("----MEAL CONTROLLER----\n");
            Console.WriteLine("Get");

            var firstMealId = 0;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"https://localhost:7104/api/meals").Result;
                var res = response.Content.ReadAsStringAsync().Result;
                var rez2 = response.Content.ReadFromJsonAsync<List<Meal>>().Result;
                foreach (Meal p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name} {p.Price}");

                firstMealId = rez2.First().Id;
            }

            Console.WriteLine("Get completed\n");

            Meal product = new Meal() { Id = 300, Name = "NewNameName", Price = 5.7M };

            Console.WriteLine("Post");
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(@"https://localhost:7104/api/meals", product).Result;
                var statusCode = response.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

            Console.WriteLine("Delete");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/meals").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Meal>>().Result;

                var id = rez2.Last().Id;
                var response = client.DeleteAsync(@$"https://localhost:7104/api/meals/{id}").Result;
                var statusCode = response.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

            Ingredient ingrToAdd;
            Console.WriteLine("Get available to add ingr");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/meals/GetAddableMenuIngredients?id=2").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Ingredient>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name}");
                ingrToAdd = rez2.First();
            }
            Console.WriteLine();


            Console.WriteLine("Add ingr");
            using (var client = new HttpClient())
            {
                var getResponse = client.PutAsJsonAsync(@"https://localhost:7104/api/meals/AddIngredient?mealId=2", ingrToAdd).Result;
                var statusCode = getResponse.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();


            Ingredient ingrToRemove;
            Console.WriteLine("Get in meal ingr");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/meals/GetInMenuIngredients?id=2").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Ingredient>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name}");

                ingrToRemove = rez2.Last();
            }
            Console.WriteLine();

            Console.WriteLine("Remove ingr");
            using (var client = new HttpClient())
            {
                var getResponse = client.PutAsJsonAsync(@"https://localhost:7104/api/meals/RemoveIngredient?mealId=2", ingrToRemove).Result;
                var statusCode = getResponse.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

        }

        private static void MenuControllerRequests()
        {
            Console.WriteLine("----MENU CONTROLLER----\n");
            Console.WriteLine("Get");

            using (var client = new HttpClient())
            {
                //Console.WriteLine(path + "api/products"); 
                var response = client.GetAsync(@"https://localhost:7104/api/menus").Result;
                var res = response.Content.ReadAsStringAsync().Result;
                var rez2 = response.Content.ReadFromJsonAsync<List<Menu>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name}");
            }

            Console.WriteLine("Get completed\n");

            var product = new Menu() { Id = 300, Name = "NewNameName" };

            Console.WriteLine("Post");
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(@"https://localhost:7104/api/menus", product).Result;
                var statusCode = response.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

            Console.WriteLine("Delete");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/menus").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Menu>>().Result;

                var id = rez2.Last().Id;
                var response = client.DeleteAsync(@$"https://localhost:7104/api/menus/{id}").Result;
                var statusCode = response.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

            Meal mealToAdd;
            Console.WriteLine("Get available to add meals");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/menus/GetAddableMenuMeals?id=8").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Meal>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name}");
                mealToAdd = rez2.First();
            }
            Console.WriteLine();


            Console.WriteLine("Add meal");
            using (var client = new HttpClient())
            {
                var getResponse = client.PutAsJsonAsync(@"https://localhost:7104/api/menus/AddMeal?menuId=8", mealToAdd).Result;
                var statusCode = getResponse.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();


            Meal mealToRemove;
            Console.WriteLine("Get in menu meals");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/menus/GetInMenuMeals?id=8").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Meal>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name}");

                mealToRemove = rez2.Last();
            }
            Console.WriteLine();

            Console.WriteLine("Remove meal");
            using (var client = new HttpClient())
            {
                var getResponse = client.PutAsJsonAsync(@"https://localhost:7104/api/menus/RemoveMeal?menuId=8", mealToRemove).Result;
                var statusCode = getResponse.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

        }

        private static void OrderControllerRequests()
        {
            Console.WriteLine("----ORDER CONTROLLER----\n");
            
            Console.WriteLine("Post");
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(@"https://localhost:7104/api/orders", null).Result;
                var statusCode = response.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();


            Console.WriteLine("Get");
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"https://localhost:7104/api/orders").Result;
                var res = response.Content.ReadAsStringAsync().Result;
                var rez2 = response.Content.ReadFromJsonAsync<List<OrderRecord>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"Order#{p.OrderNumber}, Total price: {p.Price}");
            }
            Console.WriteLine("Get completed\n");

            Meal mealToAdd;
            Console.WriteLine("Get meal to add");
            using (var client = new HttpClient())
            {
                var getResponse = client.GetAsync(@"https://localhost:7104/api/meals").Result;
                var rez2 = getResponse.Content.ReadFromJsonAsync<List<Meal>>().Result;
                foreach (var p in rez2)
                    Console.WriteLine($"{p.Id} {p.Name}");
                mealToAdd = rez2.First();
            }
            Console.WriteLine();

            Console.WriteLine("Add meal");
            using (var client = new HttpClient())
            {
                var getResponse = client.PostAsJsonAsync(@"https://localhost:7104/api/orders/AddMealToOrder?orderId=1", mealToAdd).Result;
                var statusCode = getResponse.StatusCode.ToString();
                Console.WriteLine(statusCode.ToString());
            }
            Console.WriteLine();

        }
    }
}
