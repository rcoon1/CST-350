using ASPCoreFirstApp.Models;
using ASPCoreFirstApp.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ASPCoreFirstApp.Controllers
{
    public class ProductsController : Controller
    {
        ProductDAO repository = new ProductDAO();

        public ProductsController()
        {
            repository = new ProductDAO();
        }
        public IActionResult Index()
        {
            return View(repository.AllProducts());
        }
        public IActionResult SearchResults(string searchTerm)
        {
            List<ProductModel> productList = repository.SearchProducts(searchTerm);
            return View("Index", productList);
        }
        public IActionResult ShowOneProduct(int Id)
        {
            return View(repository.GetProductById(Id));
        }
        public IActionResult ShowEditForm(int Id)
        {
            return View(repository.GetProductById(Id));
        }

        public IActionResult ProcessEdit(ProductModel product)
        {
            repository.Update(product);
            return View("Index", repository.AllProducts());
        }
        public IActionResult SearchForm() 
        {
            return View();
        }
        public IActionResult Welcome()
        {
            ViewBag.name = "Ryan";
            ViewBag.secretNumber = 8675309;
            return View();
        }
        public IActionResult Delete(int Id)
        {
            ProductDAO productDAO = new ProductDAO();
            productDAO.Delete(Id);

            List<ProductModel> products = productDAO.AllProducts();

            return View("Index", products);
        }
        public IActionResult ShowOneProductJSON(int Id)
        {
            return Json(repository.GetProductById(Id));
        }
        public IActionResult ProcessEditReturnPartial(ProductModel product)
        {
            repository.Update(product);
            return PartialView("_productCard", product);
        }
    }
}
