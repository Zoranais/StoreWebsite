using Microsoft.AspNetCore.Mvc;
using TestStore.Models;

using TestStore.Models.ViewModels;
namespace TestStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 3;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        public IActionResult List(string category, int productPage = 1)
        {
            if (category == String.Empty || category == null)
            {
                return View(new ProductsListViewModel
                {

                    Products = repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    },
                    CurrentCategory = category
                });
            }
            return View(new ProductsListViewModel
            {

                Products = repository.Products
                .Where(p => p.Category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products
                    .Where(p => p.Category == category)
                    .Count()
                },
                CurrentCategory = category
            });
        }

    }
}
