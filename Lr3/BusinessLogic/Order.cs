using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class Order
    {
        public List<OrderRecord> Records { get; set; } = new List<OrderRecord>();

        public decimal TotalPrice => Records.Sum(record => record.PortionRecord.Price);
        
        public event Action OrderUpdated;

        public void AddMeal(Meal meal, PortionRecord portionRecord, int addedCount)
        {
            var recordForMeal =
                Records.FirstOrDefault(record => record.Meal == meal && record.PortionRecord == portionRecord);

            if (recordForMeal == null)
            {
                recordForMeal = new OrderRecord()
                {
                    Meal = meal,
                    PortionRecord = portionRecord,
                };
                Records.Add(recordForMeal);
            }

            recordForMeal.RequiredCount += addedCount;

            OrderUpdated?.Invoke();
        }

        public void RemoveMeal(Meal meal, PortionRecord portionRecord, int removedCount)
        {
            var recordForMeal =
                Records.FirstOrDefault(record => record.Meal == meal && record.PortionRecord == portionRecord);

            if (recordForMeal == null)
                throw new ArgumentNullException();

            if (recordForMeal.RequiredCount < removedCount)
                throw new ArithmeticException();

            recordForMeal.RequiredCount -= removedCount;
            
            if (recordForMeal.RequiredCount > 0)
            {
                OrderUpdated?.Invoke();
                return;
            }

            Records.Remove(recordForMeal);
            OrderUpdated?.Invoke();
        }
    }
}
