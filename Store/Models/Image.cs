namespace TestStore.Models
{
    public class Image
    {
        public int? ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public List<Product> Products {get;set;}=new List<Product>();

        public string GetImageUrl() 
        {
            return string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(ImageData));
        }
    }
}
