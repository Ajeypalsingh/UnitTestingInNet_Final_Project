using EcommerceApp.Data;

namespace EcommerceApp.Models.BusinessLogicLayer
{
    public class CartBLL
    {

        private ICartRepository<Cart> _cartRepository;

        public CartBLL( ICartRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Cart GetCart()
        {
            Cart cartForProducts = _cartRepository.GetCart();

            if (cartForProducts == null)
            {
                throw new NullReferenceException("Cart not found");
            }
            else
            {
                return cartForProducts;
            }
        }

        public void RemoveProductFromCart(Guid productId)
        {
            _cartRepository.RemoveFromCart(productId);
        }

        public ICollection<CartItems> GetAllCartItems()
        {
            return _cartRepository.GetAllCartItem();
        }

        // It should have method to show sum of total price in items in cart 

        public decimal GetCartPrice()
        {
             return _cartRepository.SumOfAllItemPriceInCart();
        }

        public ICollection<Country> GetAllCountries()
        {
            return _cartRepository.GetAllCountries();
        }

        public Country GetCountry(int? id)
        {
            Country countryFound = _cartRepository.GetCountry(id);
            return countryFound;

        }

        public void AddOrder(Order order)
        {
            _cartRepository.AddOrder(order);
        }

        public ICollection <Order> GetAllOrders()
        {
            return _cartRepository.GetAllOrders();
        }

        public void ClearCart()
        {
            _cartRepository.ClearCart();
        }
    }
}
