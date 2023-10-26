using EcommerceApp.Data;
using EcommerceApp.Models.BusinessLogicLayer;
using EcommerceApp.Models;
using Moq;
using Microsoft.CodeAnalysis;

namespace ECommerceUnitTesting
{
    [TestClass]
    public class CatalogueBLLTests
    {
        [TestMethod]
        public void GetProduct_ProductFound_ReturnsProduct()
        {
            // Arrange
            Mock<IProductRepository<Product>> mockProductRepository = new Mock<IProductRepository<Product>>();
            Guid productId = Guid.NewGuid();
            Product expectedProduct = new Product { ProductId = productId, ProductName ="Samsung", AvailableQuantity = 5, PriceInCAD = 200, ProductDescription = "Innovative and futuristic design" };
            mockProductRepository.Setup(repo => repo.Get(productId)).Returns(expectedProduct);
            CatalogueBLL catalogueBLL = new CatalogueBLL(mockProductRepository.Object);

            // Act
            Product result = catalogueBLL.GetProduct(productId);

            // Assert
            Assert.AreEqual(expectedProduct, result);
        }

        [TestMethod]
        public void GetProduct_ProductNotFound_ThrowsNullReferenceException()
        {
            // Arrange
            Mock<IProductRepository<Product>> mockProductRepository = new Mock<IProductRepository<Product>>();
            mockProductRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns((Product)null);
            CatalogueBLL catalogueBLL = new CatalogueBLL(mockProductRepository.Object);


            //Act and Assert 
            Assert.ThrowsException<NullReferenceException>(() => catalogueBLL.GetProduct(Guid.NewGuid()));
        }

        // Get All Products

        [TestMethod]
        public void GetAllProduct_ProductsFound_ReturnsProducts()
        {
            // Arrange
            Mock<IProductRepository<Product>> mockProductRepository = new Mock<IProductRepository<Product>>();
            Guid productId1 = Guid.NewGuid();
            Guid productId2 = Guid.NewGuid();

            ICollection<Product> expectedProducts = new List<Product>
            {
                new Product { ProductId = productId1, ProductName ="Samsung", AvailableQuantity = 5, PriceInCAD = 200, ProductDescription = "Innovative and futuristic design" },
                new Product { ProductId = productId2, ProductName ="Google", AvailableQuantity = 10, PriceInCAD = 20, ProductDescription = "Innovative and unique features" }
            };
            mockProductRepository.Setup(repo => repo.GetAll()).Returns(expectedProducts);
            CatalogueBLL catalogueBLL = new CatalogueBLL(mockProductRepository.Object);

            // Act
            ICollection<Product> result = catalogueBLL.GetAllProduct();

            // Assert
            Assert.AreEqual(expectedProducts, result);
        }

        // Search method test
        [TestMethod]
        public void SearchItems_ProductsFound_ReturnsProducts()
        {
            // Arrange
            Mock<IProductRepository<Product>> mockProductRepository = new Mock<IProductRepository<Product>>();
            string searchQuery = "search";
            ICollection<Product> expectedProducts = new List<Product>
            {
                new Product { ProductId = Guid.NewGuid(), ProductName ="Samsung", AvailableQuantity = 5, PriceInCAD = 200, ProductDescription = "Innovative and futuristic design" },
                new Product { ProductId = Guid.NewGuid(), ProductName ="Google", AvailableQuantity = 10, PriceInCAD = 20, ProductDescription = "Innovative and unique features" }
            };
            mockProductRepository.Setup(repo => repo.SearchProduct(searchQuery)).Returns(expectedProducts);
            CatalogueBLL catalogueBLL = new CatalogueBLL(mockProductRepository.Object);

            // Act
            ICollection<Product> result = catalogueBLL.SearchItems(searchQuery);

            // Assert
            CollectionAssert.AreEqual(expectedProducts.ToList(), result.ToList());
        }

        // Add to cart test
        [TestMethod]
        public void AddToCart_ProductAvailableInCart_CartItemsQuantityIncreased()
        {
            // Arrange
            Mock<EcommerceAppContext> mockContext = new Mock<EcommerceAppContext>();
            Mock<IProductRepository<Product>> mockRepository = new Mock<IProductRepository<Product>>();

            Product product = new Product
            {
                ProductId = Guid.NewGuid(),
                AvailableQuantity = 5, 
                                       
            };

            Cart cart = new Cart();
            CartItems cartItems = new CartItems
            {
                ProductId = product.ProductId,
                CartId = cart.CartId,
                Quantity = 1
            };

            mockRepository.Setup(repo => repo.Get(product.ProductId)).Returns(product);
            mockRepository.Setup(repo => repo.AddToCart(product.ProductId)).Callback(() =>
            {
                cart.CartItems.Add(cartItems);
                product.AvailableQuantity--;
            });

            // Act
            mockRepository.Object.AddToCart(product.ProductId);

            // Assert
            Assert.AreEqual(4, product.AvailableQuantity); 
            Assert.AreEqual(1, cartItems.Quantity);
        }
    }

    [TestClass]
    public class CartBLLTests
    {
        [TestMethod]
        public void RemoveProductFromCart_ProductInCart_RemoveFromCart()
        {
            // Arrange
            Mock<EcommerceAppContext> mockContext = new Mock<EcommerceAppContext>();
            Mock<ICartRepository<Cart>> mockRepository = new Mock<ICartRepository<Cart>>();

            CartBLL cartBll = new CartBLL(mockRepository.Object);
            Guid productId = Guid.NewGuid();

            // Act
            cartBll.RemoveProductFromCart(productId);

            // Assert
            mockRepository.Verify(repo => repo.RemoveFromCart(productId), Times.Once());
        }

        [TestMethod]
        public void GetCart_CartExists_ReturnsCart()
        {
            // Arrange
            Mock<EcommerceAppContext> mockContext = new Mock<EcommerceAppContext>();
            Mock<ICartRepository<Cart>> mockRepository = new Mock<ICartRepository<Cart>>();
            CartBLL cartBll = new CartBLL(mockRepository.Object);

            Cart expectedCart = new Cart();
            mockRepository.Setup(repo => repo.GetCart()).Returns(expectedCart);

            // Act
            Cart result = cartBll.GetCart();

            // Assert
            Assert.AreEqual(expectedCart, result);
        }

        [TestMethod]
        public void GetCart_CartDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            Mock<EcommerceAppContext> mockContext = new Mock<EcommerceAppContext>();
            Mock<ICartRepository<Cart>> mockRepository = new Mock<ICartRepository<Cart>>();
            CartBLL cartBll = new CartBLL(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetCart()).Returns((Cart)null);

            // Act and assert
            Assert.ThrowsException<NullReferenceException>(() => cartBll.GetCart());
        }

    }
}