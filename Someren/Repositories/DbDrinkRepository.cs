using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Someren.Models;

namespace Someren.Repositories
{
    public class DbDrinkRepository : IDrinkRepository
    {
        private string connectionString;

        public DbDrinkRepository(string connStr)
        {
            connectionString = connStr;
        }

        public void Add(Drink drink)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Drink (Name, VAType, Stock) VALUES (@name, @vat, @stock)", conn);
                cmd.Parameters.AddWithValue("@name", drink.Name);
                cmd.Parameters.AddWithValue("@vat", drink.VAType);
                cmd.Parameters.AddWithValue("@stock", drink.Stock);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Drink> GetAllDrinks()
        {
            List<Drink> drinks = new List<Drink>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT DrinkID, Name, VAType, Stock FROM Drink", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Drink drink = new Drink
                    {
                        DrinkID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        VAType = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Stock = reader.GetInt32(3)
                    };

                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public Drink GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DrinkID, Name, VAType, Stock FROM Drink WHERE DrinkID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Drink
                    {
                        DrinkID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        VAType = reader.GetString(2),
                        Stock = reader.GetInt32(3)
                    };
                }

                return null;
            }
        }
      

        public void Update(Drink drink)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Drink SET Name = @name, VAType = @vat, Stock = @stock WHERE DrinkID = @id", conn);

                cmd.Parameters.AddWithValue("@name", drink.Name);
                cmd.Parameters.AddWithValue("@vat", drink.VAType);
                cmd.Parameters.AddWithValue("@stock", drink.Stock);
                cmd.Parameters.AddWithValue("@id", drink.DrinkID);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
