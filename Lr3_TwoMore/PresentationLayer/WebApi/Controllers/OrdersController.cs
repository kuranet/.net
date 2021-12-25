using BuisnessLogic;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public List<OrderRecord> Get()
        {
            var order = OrderController.Instance.Orders;
            return order;
        }

        [HttpGet("{id}")]
        public OrderRecord Get(int id)
        {
            var meal = OrderController.Instance.Orders.FirstOrDefault((p) => p.OrderNumber == id);
            return meal;
        }

        [HttpPost]
        public ActionResult<OrderRecord> Post()
        {
            var newOrder = OrderController.Instance.StartNewOrder();
            return Ok(newOrder);
        }

        [HttpPost]
        [Route("AddMealToOrder")]
        public ActionResult<OrderRecord> AddMeal(int orderId, [FromBody]Meal meal)
        {
            var order = OrderController.Instance.Orders.FirstOrDefault((p) => p.OrderNumber == orderId);
            OrderController.Instance.AddMealToOrder(order, meal, 1);
            return Ok(order);
        }

        [HttpPost]
        [Route("RemoveMealFromOrder")]
        public ActionResult<OrderRecord> RemoveMeal(int orderId, [FromBody] Meal meal)
        {
            var order = OrderController.Instance.Orders.FirstOrDefault((p) => p.OrderNumber == orderId);
            OrderController.Instance.RemoveMealFromOrder(order, meal, 1);
            return Ok(order);
        }
    }
}
