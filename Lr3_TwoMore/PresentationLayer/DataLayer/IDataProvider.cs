namespace DataLayer
{
    public interface IDataProvider : IDisposable
    {
        IRepository<Menu> Menus { get; }
        IRepository<Meal> Meals { get; }
        IRepository<Ingredient> Ingredients { get; }
    }
}
