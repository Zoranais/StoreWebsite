using Microsoft.AspNetCore.Mvc;
using TestStore.Models;
using System.Linq;
using Newtonsoft.Json;
using TestStore.Infrastructure;
using TestStore.Models.ViewModels;

namespace TestStore.Controllers
{
    public class CartController:Controller
    {
        private IProductRepository _repository;
        private Cart cart;
        public CartController(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            cart = cartService;
        }
        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            }); ;

        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product=_repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product=_repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new {returnUrl});
        }
    }
}
