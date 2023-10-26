using EcommerceApp.Models;

namespace EcommerceApp.Data
{
    public interface ICartRepository<T> where T : class
    {
        public T GetCart();
        public void RemoveFromCart(Guid id);
        public ICollection<CartItems> GetAllCartItem();
        public ICollection<Country> GetAllCountries();
        public Country GetCountry(int? id);
        public decimal SumOfAllItemPriceInCart();
        public void AddOrder(Order order);
        public ICollection<Order> GetAllOrders();
        public void ClearCart();
    }  
}
