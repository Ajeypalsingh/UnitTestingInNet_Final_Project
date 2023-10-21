using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [Display (Name ="Country Name")]
        [StringLength (50, MinimumLength = 3)]
        public string CountryName { get; set; }


        [Required]
        [Display(Name = "Conversion Rate")]
        public decimal CoversionRate { get; set; }


        [Required]
        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }

    }
}
