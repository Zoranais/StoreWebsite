using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestStore.Models;

namespace TestStore.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private IProductRepository _repository;
        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View<IOrderedQueryable>(_repository.Products.Select(p => p.Category)
                .Distinct().OrderBy(x=>x));
        }
    }
}
