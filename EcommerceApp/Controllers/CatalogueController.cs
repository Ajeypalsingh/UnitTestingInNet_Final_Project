using EcommerceApp.Data;
using EcommerceApp.Models;
using EcommerceApp.Models.BusinessLogicLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class CatalogueController : Controller
    {
        public CatalogueBLL _cataloguebll;
        public CatalogueController(IProductRepository<Product> productRepo)
        {
            _cataloguebll = new CatalogueBLL(productRepo);
        }

        public IActionResult Index()
        {
            try
            {
                ICollection<Product> allProducts = _cataloguebll.GetAllProduct();
                return View(allProducts);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public IActionResult AddToCart(Guid productId)
        {
            try
            {
                _cataloguebll.AddProductToCart(productId);
                return RedirectToAction("Index");
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public IActionResult SearchProducts(string searchQuery)
         {
            try
            {
               ICollection<Product> searchedProducts = _cataloguebll.SearchItems(searchQuery);
                return View("Index", searchedProducts);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
