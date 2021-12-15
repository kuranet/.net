using BuisnessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class OrderEditingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Orders = OrderController.Instance.Orders;
            return View();
        }

        [HttpPost]
        public void AddOrder()
        {
            OrderController.Instance.StartNewOrder();
        }
    }
}