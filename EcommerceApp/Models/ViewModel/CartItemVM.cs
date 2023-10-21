using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceApp.Models.ViewModel
{
    public class CartItemVM
    {
        public ICollection<CartItems> CartItem { get; set; }
        public decimal TotalPrice { get; set; }
        public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();

        public CartItemVM(ICollection<Country> countries)
        {
            foreach (Country country in countries)
            {
                SelectListItems.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryId.ToString() });
            }
        }
    }
}
