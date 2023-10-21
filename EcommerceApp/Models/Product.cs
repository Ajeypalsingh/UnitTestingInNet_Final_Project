using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Product Name must be between 3 and 30 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Description is required.")]
        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "Product Description cannot exceed 100 characters.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal PriceInCAD { get; set; }

        [Required(ErrorMessage = "Available Quantity is required.")]
        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Available Quantity must be a positive integer.")]
        public int AvailableQuantity { get; set; }

        public List<CartItems> CartItems { get; set; } = new List<CartItems>();
    }
}
