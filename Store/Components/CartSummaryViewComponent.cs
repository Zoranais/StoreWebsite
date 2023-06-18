using Microsoft.AspNetCore.Mvc;
using TestStore.Models;

namespace TestStore.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        private Cart _cart;
        public CartSummaryViewComponent(Cart cart)
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
