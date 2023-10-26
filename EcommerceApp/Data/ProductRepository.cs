using EcommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data
{
    public class ProductRepository : IProductRepository<Product>
    {

        private readonly EcommerceAppContext _context;

        public ProductRepository(EcommerceAppContext context)
        {
            _context = context;
        }


        public Product Get(Guid id)
        {
            Product productToFind = _context.Product.FirstOrDefault(p => p.ProductId == id);

            return productToFind;
        }

        public ICollection<Product> GetAll()
        {
            return _context.Product.OrderBy(p => p.ProductName).ToList();
        }

        public void AddToCart(Guid id)
        {
            Product productToAdd = Get(id);
            Cart cart = _context.Carts.FirstOrDefault();

            // if product is already in cart just increment quantity and if not add new cartitems in cart

            if (productToAdd.AvailableQuantity > 0)
            {
                CartItems existingItems = _context.CartItems.FirstOrDefault(ci => ci.ProductId == productToAdd.ProductId && ci.CartId == cart.CartId);
                if (existingItems != null)
                {
                    existingItems.Quantity++;
                }
                else
                {
                    CartItems newItem = new CartItems
                    {
                        Cart = cart,
                        CartId = cart.CartId,
                        Product = productToAdd,
                        ProductId = productToAdd.ProductId,
                        Quantity = 1
                    };
                    _context.CartItems.Add(newItem);
                }
                productToAdd.AvailableQuantity--;
                _context.SaveChanges();
            } 
        }

        public ICollection<Product> SearchProduct(string query)
        {
            ICollection<Product> products = _context.Product
                                                    .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                                                    .OrderBy(p => p.ProductName)
                                                    .ToHashSet(); ;
            return products;
        }
    }
}
