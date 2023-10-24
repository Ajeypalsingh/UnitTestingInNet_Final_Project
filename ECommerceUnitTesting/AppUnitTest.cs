using EcommerceApp.Data;
using EcommerceApp.Models.BusinessLogicLayer;
using EcommerceApp.Models;
using Moq;

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
            var result = catalogueBLL.GetProduct(productId);

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

            // Act
            

            //Act Assert 
            Assert.ThrowsException<NullReferenceException>(() => catalogueBLL.GetProduct(Guid.NewGuid()));
        }
    }
}