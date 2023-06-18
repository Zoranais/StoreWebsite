using Microsoft.EntityFrameworkCore;

namespace TestStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
            foreach (var product in Products)
            {
                if (product.Images.Count==0)
                {
                    product.Images.Add(_context.Images.FirstOrDefault());
                    SaveProduct(product);
                }
            }
        }
        public IQueryable<Product> Products => _context.Products.Include(i=>i.Images).ThenInclude(o=>o.Products);
        public void SaveProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry=_context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if(dbEntry != null)
                {
                    dbEntry.ProductName=product.ProductName;
                    dbEntry.Price=product.Price;
                    dbEntry.Description=product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.Images = product.Images;
                }
            }
            _context.SaveChanges();
        }
        public Product DeleteProduct(int productId)
        {
            Product dbEntry=_context.Products.FirstOrDefault(p=>p.ProductId == productId);
            if (dbEntry!=null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
        public void AddImage(int productId, int imageId)
        {
            Product dbEntryProduct=_context.Products.FirstOrDefault(p=>p.ProductId==productId);
            Image dbEntryImage=_context.Images.FirstOrDefault(i=>i.ImageId==imageId);
            if (dbEntryImage!=null&&dbEntryProduct!=null)
            {
                dbEntryProduct.Images.Add(dbEntryImage);
                _context.SaveChanges();
            }
        }
        public void RemoveImage(int productId, int imageId)
        {
            Product dbEntryProduct = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            Image dbEntryImage = _context.Images.FirstOrDefault(i => i.ImageId == imageId);
            if (dbEntryImage != null && dbEntryProduct != null)
            {
                dbEntryProduct.Images.Remove(dbEntryImage);
                _context.SaveChanges();
            }
        }
    }
}
