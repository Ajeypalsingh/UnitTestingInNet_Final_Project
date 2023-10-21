using EcommerceApp.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data
{
    public class CartRepository : ICartRepository<Cart>
    {
        private readonly IProductRepository<Product> _productRepository;

        private readonly EcommerceAppContext _context;
        public CartRepository(EcommerceAppContext context, IProductRepository<Product> productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        public Cart GetCart()
        {
            Cart cartForProducts = _context.Carts.FirstOrDefault();
            if (cartForProducts == null)
            {
                throw new NullReferenceException();
            }
            return cartForProducts;
        }

        public void RemoveFromCart(Guid productId)
        {
            Product product = _productRepository.Get(productId);
            Cart cart = GetCart();

            // If quantity of item is more than 0 then decrement its quantity and if it becomes 0 remove object from list.
            CartItems itemToDelete = _context.CartItems.FirstOrDefault(ci => ci.ProductId == productId && ci.CartId == cart.CartId);

            if (itemToDelete != null)
            {
                if(itemToDelete.Quantity > 0)
                {
                    itemToDelete.Quantity--;
                }

                if (itemToDelete.Quantity == 0)
                {
                    _context.CartItems.Remove(itemToDelete);
                }
                product.AvailableQuantity++;
                _context.SaveChanges();
            }
        }

        public ICollection<CartItems> GetAllCartItem()
        {
            List<CartItems> addedItems = _context.CartItems.Include(ci => ci.Product).ToList();
            return addedItems;
        }

        public ICollection<Country> GetAllCountries()
        {
            List<Country> allCountries = _context.Country.ToList();
            return allCountries;
        }
        public decimal SumOfAllItemPriceInCart()
        {
            ICollection<CartItems> cartItems = GetAllCartItem();

            decimal totalPrice = 0;

            foreach (CartItems cartItem in cartItems)
            {
                totalPrice +=cartItem.Quantity * cartItem.Product.PriceInCAD;
            }

            return totalPrice;
        }

        public Country GetCountry(int? id)
        {
            Country foundCountry = _context.Country.FirstOrDefault(c => c.CountryId == id);
            
            return foundCountry;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public ICollection<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void ClearCart()
        {
            ICollection<CartItems> itemsToRemove = GetAllCartItem();
            _context.CartItems.RemoveRange(itemsToRemove);
            _context.SaveChanges();
        }

    }
}
