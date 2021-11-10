namespace BusinessLogic
{
    public class OrderRecord
    {
        public Meal Meal { get; set; }
        public PortionRecord PortionRecord { get; set; }
        public int RequiredCount { get; set; }
    }
}
