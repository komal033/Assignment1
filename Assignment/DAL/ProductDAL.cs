using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assignment.DAL
{
    public class ProductDAL
    {
        private readonly string connectionString = "Data Source=DESKTOP-QLCNTQ7\\SQLEXPRESS;Initial Catalog=assignment;Integrated Security=True;Encrypt=False";

        // Method to get all products
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT ProductId, ProductName, CategoryId FROM Product", connection);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    };
                    products.Add(product);
                }
            }
            return products;
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
    
