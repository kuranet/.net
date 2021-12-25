namespace ConsoleClient
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Meal> Meals { get; set; } = new List<Meal>();

    }
}
