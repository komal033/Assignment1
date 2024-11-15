using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Assignment.Models;
using Dapper;
using System.Linq;
using Assignment.Models;

namespace Assignment.ProductDAL
{
    public class ProductDAL
    {
        private readonly string connectionString = "Data Source=DESKTOP-QLCNTQ7\\SQLEXPRESS;Initial Catalog=assignment;Integrated Security=True;Encrypt=False ";

        // Method to get all products
        public IEnumerable<Product> GetProducts(int pageNumber, int pageSize)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Ensure connection is opened before querying
                var query = @"
            SELECT * FROM (
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY ProductId) AS RowNum, 
                    ProductId, ProductName, CategoryId
                FROM Product
            ) AS Paginated
            WHERE RowNum BETWEEN @StartRow AND @EndRow";

                var parameters = new
                {
                    StartRow = (pageNumber - 1) * pageSize + 1,
                    EndRow = pageNumber * pageSize
                };

                return connection.Query<Product>(query, parameters).ToList();
            }
        }

        // Method to get a product by its ID
        public Product GetProductById(int productId)
        {
            Product product = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT ProductId, ProductName, CategoryId FROM Product WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    };
                }
            }
            return product;
        }

        // Method to add a new product
        public void AddProduct(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO Product (ProductName, CategoryId) VALUES (@ProductName, @CategoryId)", connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Method to update an existing product
        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("UPDATE Product SET ProductName = @ProductName, CategoryId = @CategoryId WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Method to delete a product
        public void DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM Product WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
