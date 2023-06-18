//using Moq;
//using TestStore.Models;
//using TestStore.Controllers;
//using Xunit;
//using System.Linq;
//using System.Collections.Generic;

//namespace TestStore.Tests
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public void Test1()
//        {
//            Mock<IProductRepository> mock= new Mock<IProductRepository>();
//            mock.Setup(m=>m.Products).Returns((new Product[] { 
//                new Product
//                {
//                    ProductId = 1, ProductName="P1"
//                },
//                new Product
//                {
//                    ProductId = 2, ProductName="P2"
//                },
//                new Product
//                {
//                    ProductId = 3, ProductName="P3"
//                },
//                new Product
//                {
//                    ProductId = 4, ProductName="P4"
//                },
//                new Product
//                {
//                    ProductId = 5, ProductName="P5"
//                }
                

//            }).AsQueryable<Product>());
//            ProductController productController = new ProductController(mock.Object);
//            productController.PageSize = 3;

//            IEnumerable<Product> result =
//                productController.List(2).ViewData.Model
//                as IEnumerable<Product>;
//        }
//    }
//}