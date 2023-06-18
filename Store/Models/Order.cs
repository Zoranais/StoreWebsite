using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TestStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine>? Lines { get; set; }
        
        [BindNever]
        public bool isShipped { get; set; }
        [Required(ErrorMessage="Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the first address line")]
        public string Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        [Required(ErrorMessage = "Enter a country name")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Enter a sity name")]
        public string Sity { get; set; }
        public string? Zip { get; set; }
        public bool GiftWrap { get;set; }

    }
}
