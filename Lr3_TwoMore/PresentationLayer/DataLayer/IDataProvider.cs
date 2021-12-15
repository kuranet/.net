namespace DataLayer
{
    public interface IDataProvider
    {
        IList<Menu> Menus { get; }
        IList<Meal> Meals { get; }
        IList<Ingredient> Ingredients { get; }
    }
}
