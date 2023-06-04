using ASPCoreFirstApp.Models;
using ASPCoreFirstApp.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ASPCoreFirstApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductsAPIController : ControllerBase
    {

        ProductDAO repository = new ProductDAO();

        public ProductsAPIController()
        {
            repository = new ProductDAO();
        }


        [HttpGet]
        public IEnumerable<ProductDTO> Index()
        {
            // get the products
            List<ProductModel> productList = repository.AllProducts();
            // translate into DTO
            IEnumerable<ProductDTO> productDTOList = from p in productList
                                                     select
                                                     new ProductDTO(p.Id, p.Name, p.Price, p.Description);
            return productDTOList;

        }


        [HttpGet("searchresults/{searchTerm}")]
        // respond to search results
        public IEnumerable<ProductDTO> SearchResults(string searchTerm)
        {
            List<ProductModel> productList = repository.SearchProducts(searchTerm);
            //return productList;
            // translate into DTO
            List<ProductDTO> productDTOList = new List<ProductDTO>();
            foreach (ProductModel p in productList)
            {
                productDTOList.Add(new ProductDTO(p.Id, p.Name, p.Price, p.Description));
            }
            return productDTOList;

        }



        [HttpGet("showoneproduct/{Id}")]
        //[ResponseType(typeof(ProductDTO))]
        public ActionResult<ProductDTO> ShowOneProduct(int Id)
        {
            // get the correct product from the database
            ProductModel product = repository.GetProductById(Id);

            // create a new DTO based on the product
            ProductDTO productDTO = new ProductDTO(product.Id, product.Name, product.Price, product.Description);

            // return the DTO insted of the product
            return productDTO;
        }



        [HttpPut("processedit")]
        public IEnumerable<ProductDTO> ProcessEdit(ProductModel product)
        {
            repository.Update(product);
            List<ProductModel> productList = repository.AllProducts();

            // translate into DTO
            IEnumerable<ProductDTO> productDTOList = from p in productList
                                                     select new ProductDTO(p.Id, p.Name, p.Price, p.Description);

            return productDTOList;
        }

        [HttpPut("ProcessEditReturnOneItem")]
        public ActionResult<ProductDTO> ProcessEditReturnOneItem(ProductModel product)
        {
            repository.Update(product);
            ProductModel updatedProduct = repository.GetProductById(product.Id);
            ProductDTO productDTO = new ProductDTO(product.Id, product.Name, product.Price, product.Description);
            return productDTO;

        }
    }
}