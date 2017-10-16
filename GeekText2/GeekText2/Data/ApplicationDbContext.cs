using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using GeekText2.Models;

namespace GeekText2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public string ConnectionString { get; set; }

        public ApplicationDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();
            //PopulateDatabase();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Books", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            ISBN = reader["ISBN"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Author = reader["Author"].ToString(),
                            Genre = reader["genre"].ToString(),
                            Publisher = reader["Publisher"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Image = reader["image"].ToString(),
                            IsFeatured = Convert.ToInt32(reader["isFeatured"])

                        });
                    }
                }
            }
            return list;
        }
        public void PopulateDatabase()
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE Books SET ISBN = 978-0062315007, Title = 'The Alchemist', Description='Description7', Price=34.99, Author='Paulo Coelho', Genre='Adventure', Publisher='HarperOne', Quantity = 60, image = '\\Data\\BookCovers\\516c6gUQLaL._SX329_BO1,204,203,200_.jpg', isFeatured=1";
                cmd.ExecuteNonQuery();
            }
        }

    }
}
