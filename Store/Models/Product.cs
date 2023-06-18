using System.ComponentModel.DataAnnotations;

namespace TestStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage = "Please enter a correct price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter a product category")]
        public string Category { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
