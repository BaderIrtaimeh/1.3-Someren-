using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Someren.Models;

namespace Someren.Repositories
{
    public class DbDrinkOrderRepository : IDrinkOrderRepository
    {

        private string connectionString;

        public DbDrinkOrderRepository(string connStr)
        {
            this.connectionString = connStr;
        }

        public void AddDrinkOrder(DrinkOrder order)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                INSERT INTO DrinkOrder (StudentNumber, DrinkID, Quantity)
                VALUES (@student, @drink, @qty);

                UPDATE Drink SET Stock = Stock - @qty 
                WHERE DrinkID = @drink AND Stock >= @qty;
            ", conn);

                cmd.Parameters.AddWithValue("@student", order.StudentNumber);
                cmd.Parameters.AddWithValue("@drink", order.DrinkID);
                cmd.Parameters.AddWithValue("@qty", order.Quantity);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
