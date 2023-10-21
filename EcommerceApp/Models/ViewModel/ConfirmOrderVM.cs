using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models.ViewModel
{
    public class ConfirmOrderVM
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal PriceOfAllItems { get; set; }
        public decimal ConvertedPrice { get; set; }
        public decimal TotalPriceWithTax { get; set; }

        [Required (ErrorMessage ="Address required")] 
        public string Address { get; set; }

        [Required(ErrorMessage = "Mailing Code required")]
        public string MailingCode { get; set; }
        public int NumberOfItems { get; set; }
    }

}
