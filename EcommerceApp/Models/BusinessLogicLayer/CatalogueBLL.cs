using EcommerceApp.Data;

namespace EcommerceApp.Models.BusinessLogicLayer
{
    public class CatalogueBLL
    {

        private IProductRepository<Product> _productRepository;
       

        public CatalogueBLL(IProductRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProduct(Guid id)
        {
            Product productFound = _productRepository.Get(id);
            if (productFound == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            else
            {
                return productFound;
            }
        }

        public ICollection<Product> GetAllProduct()
        {
            ICollection<Product> productFound = _productRepository.GetAll();
            if (productFound == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            else
            {
                return productFound;
            }
        }

        public void AddProductToCart(Guid productId)
        {
            _productRepository.AddToCart(productId);
        }

        public ICollection<Product> SearchItems(string searchQuery)
        {
            ICollection<Product> searchedProducts = _productRepository.SearchProduct(searchQuery);
            return searchedProducts;

        }
    }
}
