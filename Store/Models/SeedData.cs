using Microsoft.EntityFrameworkCore;

namespace TestStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product
                        {
                            ProductName = "TestItem1",
                            Description = "This is Test Item",
                            Category = "Test1",
                            Price = 10,
                        },
                        new Product
                        {
                            ProductName = "TestItem2",
                            Description = "This is Test Item",
                            Category = "Test1",

                            Price = 10,
                        },
                        new Product
                        {
                            ProductName = "TestItem3",
                            Description = "This is Test Item",
                            Category = "Test2",

                            Price = 10,
                        },
                        new Product
                        {
                            ProductName = "TestItem4",
                            Description = "This is Test Item",
                            Category = "Test2",

                            Price = 10,
                        }

                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
