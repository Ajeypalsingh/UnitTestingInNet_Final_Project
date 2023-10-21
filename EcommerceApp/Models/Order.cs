using EcommerceApp.Models.BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(60, ErrorMessage = "Address cannot exceed 60 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [Display(Name = "Country")]
        public string DestinationCountry { get; set; }

        [Required(ErrorMessage = "Mailing Code is required.")]
        [Display(Name = "Mailing Code")]
        public string MailingCode { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ConvertedPrice { get; set; }
        public decimal PriceWithTax { get; set; }

        public int TotalItems { get; set; }
    }
}
