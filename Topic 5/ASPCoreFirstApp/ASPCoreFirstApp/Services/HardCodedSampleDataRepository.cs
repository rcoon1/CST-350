using ASPCoreFirstApp.Models;
using Bogus;

namespace ASPCoreFirstApp.Services
{
    public class HardCodedSampleDataRepository : IProductsDataService
    {
        List<ProductModel> productList = new List<ProductModel>();

        public HardCodedSampleDataRepository()
        {

            productList.Add(new ProductModel(1, "Mouse Pad", 5.99m, "A square piece of plastic to make mousing easier"));
            productList.Add(new ProductModel(2, "Web Cam", 45.50m, "Enables you to attend more zoom meetings"));
            productList.Add(new ProductModel(3, "4 TB USB Hard Drive", 130.00m, "Back up all of your work. You won't regret it."));
            productList.Add(new ProductModel(4, "Wireless Mouse", 15.99m, "Notebook mice really don't work very well. Do they?"));
            for (int i = 0; i < 100; i++)
            {
                productList.Add(new Faker<ProductModel>()
                    .RuleFor(p => p.Id, i + 5)
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(100))
                    .RuleFor(p => p.Description, f => f.Rant.Review())

                        );
            }
        }
    public List<ProductModel> AllProducts()
        {
            return productList;
        }

        public bool Delete(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public int Update(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}
