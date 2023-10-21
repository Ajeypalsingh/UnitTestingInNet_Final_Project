using EcommerceApp.Data;
using EcommerceApp.Models;
using EcommerceApp.Models.BusinessLogicLayer;
using EcommerceApp.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    public class CartController : Controller
    {
        public CartBLL _cartBLL;
        public CartController(ICartRepository<Cart> cart)
        {
            _cartBLL = new CartBLL(cart);
        }

        public IActionResult Index()
        {
            try
            {
                ICollection<CartItems> addedProducts = _cartBLL.GetAllCartItems();
                ICollection<Country> allCountries = _cartBLL.GetAllCountries();
                decimal totalPrice = _cartBLL.GetCartPrice();

                CartItemVM cartItemVM = new CartItemVM(allCountries)
                {
                    CartItem = addedProducts,
                    TotalPrice = totalPrice,
                };

                return View(cartItemVM);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public IActionResult RemoveFromCart(Guid productId)
        {
            try
            {
                _cartBLL.RemoveProductFromCart(productId);
                return RedirectToAction("Index");
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

       
        public IActionResult ConfirmOrder(int? CountryId)
        {
            if (CountryId == null)
            {
                return RedirectToAction("Index");
            }

            Country selectedCountry = _cartBLL.GetCountry(CountryId);
            int totalNumberOfItems = _cartBLL.GetAllCartItems().Count();

            decimal priceOfAllItems = _cartBLL.GetCartPrice();

            decimal convertedPrice = priceOfAllItems * selectedCountry.CoversionRate;
            decimal totalPriceWithTax = convertedPrice + (convertedPrice * selectedCountry.TaxRate);

            ConfirmOrderVM model = new ConfirmOrderVM
            {
                CountryName = selectedCountry.CountryName,
                ConversionRate = selectedCountry.CoversionRate,
                TaxRate = selectedCountry.TaxRate,
                PriceOfAllItems = priceOfAllItems,
                ConvertedPrice = convertedPrice,
                TotalPriceWithTax = totalPriceWithTax,
                NumberOfItems = totalNumberOfItems
            };

            return View(model);
        }

        
        public IActionResult CreateOrder(ConfirmOrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ConfirmOrder", model);
            }

            Order order = new Order
            {
                Address = model.Address,
                MailingCode = model.MailingCode,
                DestinationCountry = model.CountryName,
                TotalPrice = model.PriceOfAllItems,
                ConvertedPrice = model.ConvertedPrice,
                PriceWithTax = model.TotalPriceWithTax,
                TotalItems = model.NumberOfItems
            };

            _cartBLL.AddOrder(order);
            _cartBLL.ClearCart();

            return RedirectToAction("OrderSummary");
        }

        public ActionResult OrderSummary()
        {
            ICollection<Order> allOrders = _cartBLL.GetAllOrders();
            return View("OrderSummary", allOrders);
        }
    }
}
