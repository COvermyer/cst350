using ProductsApp.Models;
using System.Data.SqlClient;

namespace ProductsApp.Services
{
    public class ProductDAO : IProductDAO
    {
        private readonly string _connectionString;

        public ProductDAO(IConfiguration configuration)
        {
            _connectionString = configuration["SqlConnection:DefaultConnection"];
        }

        public async Task<int> AddProduct(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Products (Name, Price, Description, CreatedAt, ImageURL) OUTPUT INSERTED.Id VALUES (@Name, @Price, @Description, @CreatedAt, @ImageURL)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                cmd.Parameters.AddWithValue("@ImageURL", product.ImageURL);

                conn.Open();
                int newId = (int)await cmd.ExecuteScalarAsync();
                return newId; // returns the ID of the newly added product
            }
        }

        public async Task DeleteProduct(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", product.Id);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    products.Add(new ProductModel
                    {
                        Id = reader.GetInt32(0),
                        // if a column is null, return an empty string or 0 as a default value
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        Description = reader.IsDBNull(3) ? string.Empty :  reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                        ImageURL = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    });
                }
            }

            return products;// return product list
        }

        public async Task<ProductModel?> GetProductById(int id)
        {
            ProductModel model = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    model = new ProductModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        Description = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                        ImageURL = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    };
                }
            }
            return model;
        }

        public async Task<IEnumerable<ProductModel>> SearchForProductsByDescription(string searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Description LIKE @SearchTerm", conn);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                
                List<ProductModel> products = new List<ProductModel>();
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read()) {
                    products.Add(new ProductModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        Description = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                        ImageURL = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    });
                }
                return products;
            }
        }

        public async Task<IEnumerable<ProductModel>> SearchForProductsByName(string searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Name LIKE @SearchTerm", conn);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                List<ProductModel> products = new List<ProductModel>();
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    products.Add(new ProductModel
                    { 
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        Description = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                        ImageURL = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    });
                }
                return products;
            }
        }

        public async Task UpdateProduct(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Products SET Name = @Name, Price= @Price, Description = @Description, CreatedAt = @CreatedAt, ImageURL = @ImageURL WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                cmd.Parameters.AddWithValue("@ImageURL", product.ImageURL);
                cmd.Parameters.AddWithValue("@Id", product.Id);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
