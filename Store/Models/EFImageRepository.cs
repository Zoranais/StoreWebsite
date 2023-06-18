using Microsoft.EntityFrameworkCore;

namespace TestStore.Models
{
    public class EFImageRepository : IImageRepository
    {
        private ApplicationDbContext _context;
        IQueryable<Image> IImageRepository.Images => _context.Images.Include(p=>p.Products);
        public EFImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        void IImageRepository.Add(Image image)
        {
            if (image.ImageId == 0)
            {
                _context.Add(image);
            }
            else
            {
                Image dbEntry = _context.Images.FirstOrDefault(i=>i.ImageId==image.ImageId);
                if (dbEntry != null)
                {
                    dbEntry.ImageTitle = image.ImageTitle;
                    dbEntry.ImageData= image.ImageData;
                }
            }
            _context.SaveChanges();
        }
        Image IImageRepository.Remove(int imageId)
        {
            Image dbEntry= _context.Images.FirstOrDefault(i=>i.ImageId==imageId);
            if(dbEntry != null)
            {
                try
                {
                    _context.Remove(dbEntry);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
            return dbEntry;
        }
    }
}
