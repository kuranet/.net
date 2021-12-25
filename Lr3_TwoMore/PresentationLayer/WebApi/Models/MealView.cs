namespace WebApi.Models
{
    [Serializable]
    public class MealView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
