namespace EcommerceApp.Data
{
    public interface IProductRepository<T> where T : class
    {
        public T Get(Guid id);
        public ICollection<T> GetAll();
        public void AddToCart(Guid id);
        public ICollection<T> SearchProduct(string query);
    } 
}
