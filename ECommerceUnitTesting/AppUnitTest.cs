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

        [TestMethod]
        public void GetAllCartItems_ItemsInCart_ReturnsAllItems()
        {
            // Arrange
            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();
            List<CartItems> sampleCartItems = new List<CartItems>
            {
                new CartItems { Id = new Guid(), CartId = new Guid(), ProductId = new Guid() },
                new CartItems { Id = new Guid(), CartId = new Guid(), ProductId = new Guid() },
                new CartItems { Id = new Guid(), CartId = new Guid(), ProductId = new Guid() },
                new CartItems { Id = new Guid(), CartId = new Guid(), ProductId = new Guid() }
            };

            mockCartRepository.Setup(repo => repo.GetAllCartItem()).Returns(sampleCartItems);

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);


            // Act
            var result = cartBll.GetAllCartItems();

            // Assert
            Assert.AreEqual(sampleCartItems.Count, result.Count);
        }

        [TestMethod]
        public void GetCartPrice_ReturnsCorrectTotalPrice()
        {
            // Arrange
            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();


            var sampleCartItems = new List<CartItems>
            {
                new CartItems
                {
                    Product = new Product
                    {
                        PriceInCAD = 100.0M
                    },
                    Quantity = 2
                },
                new CartItems
                {
                    Product = new Product
                    {
                        PriceInCAD = 50.0M
                    },
                    Quantity = 3
                }
            };

            decimal expectedTotalPrice = 250.0M;  

            mockCartRepository.Setup(repo => repo.SumOfAllItemPriceInCart()).Returns(expectedTotalPrice);

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);

            // Act
            var actualTotalPrice = cartBll.GetCartPrice();

            // Assert
            Assert.AreEqual(expectedTotalPrice, actualTotalPrice);
        }

        [TestMethod]
        public void GetAllCountries_ReturnsAllCountries()
        {
            // Arrange
            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();
            List<Country> testCounties = new List<Country>
            {
                new Country { CountryId = 1, CountryName = "USA" },
                new Country { CountryId = 2, CountryName = "Canada" },
                new Country { CountryId = 3, CountryName = "India" },
                new Country { CountryId = 4, CountryName = "Pakistan" },
                new Country { CountryId = 5, CountryName = "Australia" },
                
            };

            mockCartRepository.Setup(repo => repo.GetAllCountries()).Returns(testCounties);

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);

            // Act
            var result = cartBll.GetAllCountries();

            // Assert
            Assert.AreEqual(testCounties.Count, result.Count);
        }

        [TestMethod]
        public void GetCountry_ValidId_ReturnsExpectedCountry()
        {
            // Arrange
            int? testId = 1;
            Country testCountry = new Country { CountryId = 1, CountryName = "USA" };

            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();
            mockCartRepository.Setup(repo => repo.GetCountry(testId)).Returns(testCountry);

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);

            // Act
            var result = cartBll.GetCountry(testId);

            // Assert
            Assert.AreEqual(testCountry, result);
        }

        [TestMethod]
        public void AddOrder_ValidOrder_IsCalled()
        {
            // Arrange
            Order testOrder = new Order();

            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();
            mockCartRepository.Setup(repo => repo.AddOrder(testOrder));

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);

            // Act
            cartBll.AddOrder(testOrder);

            // Assert
            mockCartRepository.Verify(repo => repo.AddOrder(testOrder), Times.Once);
        }


        [TestMethod]
        public void GetAllOrders_ReturnsAllOrders()
        {
            // Arrange
            List<Order> testOrders = new List<Order>
    {
        new Order(),
        new Order(),
        new Order()
    };

            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();
            mockCartRepository.Setup(repo => repo.GetAllOrders()).Returns(testOrders);

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);

            // Act
            var result = cartBll.GetAllOrders();

            // Assert
            Assert.AreEqual(testOrders.Count, result.Count);
        }

        [TestMethod]
        public void ClearCart_IsCalled()
        {
            // Arrange
            Mock<ICartRepository<Cart>> mockCartRepository = new Mock<ICartRepository<Cart>>();
            mockCartRepository.Setup(repo => repo.ClearCart());

            CartBLL cartBll = new CartBLL(mockCartRepository.Object);

            // Act
            cartBll.ClearCart();

            // Assert
            mockCartRepository.Verify(repo => repo.ClearCart(), Times.Once);
        }
    }
}