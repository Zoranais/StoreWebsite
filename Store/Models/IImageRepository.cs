namespace TestStore.Models
{
    public interface IImageRepository
    {
        IQueryable<Image> Images { get; }
        void Add(Image image);
        Image Remove(int imageId);
    }
}
