using ASPCoreFirstApp.Models;
using System.Data.SqlClient;

namespace ASPCoreFirstApp.Services
{
    public class ProductDAO : IProductsDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        public List<ProductModel> AllProducts()
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string sqlStatement = "SELECT * FROM dbo.Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel((int)reader[0], (string)reader[1], (decimal)reader[2], (string)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundProducts;
        }

        public bool Delete(ProductModel product)
        {
            throw new NotImplementedException();
        }
        internal int Delete(int Id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM dbo.Products WHERE Id = @Id";

                SqlCommand myCommand = new SqlCommand(@query, connection);

                myCommand.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = Id;

                connection.Open();

                int deleteId = myCommand.ExecuteNonQuery();

                return deleteId;



            }
        }

        public ProductModel GetProductById(int id)
        {
            {
                ProductModel foundProduct = null;

                // uses prepared statements for security. @username @password are defined below
                string sqlStatement = "SELECT * FROM dbo.Products WHERE Id = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //define the values of the two placeholders in the sqlStatement string
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            foundProduct = new ProductModel((int)reader[0], (string)(reader[1]), (decimal)reader[2], (string)reader[3]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    };
                }
                return foundProduct;
            }
        }

            public int Insert(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string sqlStatement = "SELECT * FROM dbo.Products WHERE Name LIKE @Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", "%" + searchTerm + "%");

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel((int)reader[0], (string)reader[1], (decimal)reader[2], (string)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundProducts;
        }

        public int Update(ProductModel product)
        {
            // returns -1 if the update fails
            int newIdNumber = 1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE dbo.Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";

                SqlCommand myCommand = new SqlCommand(@query, connection);
                myCommand.Parameters.AddWithValue("@Id", product.Id);
                myCommand.Parameters.AddWithValue("@Name", product.Name);
                myCommand.Parameters.AddWithValue("@Price", product.Price);
                myCommand.Parameters.AddWithValue("@Description", product.Description);


                try
                {
                    connection.Open();

                    newIdNumber = Convert.ToInt32(myCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                return newIdNumber;
            }
        }
    }
}