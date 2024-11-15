using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assignment.DAL
{
    public class CategoryDAL
    {
        // Replace with your actual database connection string
        private readonly string connectionString = "Data Source=DESKTOP-QLCNTQ7\\SQLEXPRESS;Initial Catalog=assignment;Integrated Security=True;Encrypt=False";

        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Category", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category category = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Category WHERE CategoryId = @CategoryId", connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = new Category
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return category;
        }

        public void AddCategory(Category category)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@CategoryName)", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Category SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Category WHERE CategoryId = @CategoryId", connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
    
   