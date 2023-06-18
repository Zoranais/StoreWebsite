using Microsoft.AspNetCore.Mvc;
using TestStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
namespace TestStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;
        public OrderController(IOrderRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }
        [Authorize]
        public IActionResult List() => View(
            _repository.Orders.Where(x=> !x.isShipped));
        [HttpPost]
        [Authorize]
        public IActionResult MarkAsShipped(int orderId)
        {
            Order order = _repository.Orders.FirstOrDefault(o=>o.OrderId==orderId);
            if (order != null)
            {
                order.isShipped = true;
                _repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public IActionResult Checkout()=>View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.GatCartLines.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.GatCartLines.ToArray();
                _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            _cart.ClearCart();
            return View();
        }
    }
}
