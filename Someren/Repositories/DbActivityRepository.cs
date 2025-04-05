using Microsoft.Data.SqlClient;
using Someren.Models;

namespace Someren.Repositories
{
    public class DbActivityRepository : IActivityRepository
    {
        private readonly string _connectionString;

        public DbActivityRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SomerenDatabase");
        }
        public List<Activity> GetAll()
        {
            var activities = new List<Activity>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
        SELECT 
            ActivityID, 
           Name, 
            Time
        FROM Activity
        ";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(new Activity
                    {
                        ActivityID = reader.GetInt32(reader.GetOrdinal("ActivityID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Time = reader.GetDateTime(reader.GetOrdinal("Time"))
                    });
                }
            }

            return activities;
        }
       
    }
}
