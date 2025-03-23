using Microsoft.Data.SqlClient;
using Someren.Models;

namespace Someren.Repositories
{
    
        public class DbLecturerRepository : ILecturerRepository
        {
            private readonly string _connectionString;

            public DbLecturerRepository(IConfiguration config)
            {
                _connectionString = config.GetConnectionString("SomerenDatabase");
            }

            public List<Lecturer> GetAll()
            {
                List<Lecturer> lecturers = new List<Lecturer>();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM lecturer";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                    lecturers.Add(new Lecturer
                    {
                        LecturerID = reader.GetInt32(0),
                        Name = reader.GetString(1),

                        Moderates = reader.GetBoolean(3),// boolean?? I thought we would have an enum for this (enum {event1, event2, event3, none) 
                        DateOfBirth = reader.GetDateTime(4)
                        });
                    }
                }
                return lecturers;
            }
            public void Add(Lecturer lecturer)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO lecturer (lecturerID, Name, Moderates, Dateofbirth)
                         VALUES (@lecturerID, @Name,  @Moderates, @Dateofbirth)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LecturerID", lecturer.LecturerID);
                    cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                    cmd.Parameters.AddWithValue("@Moderates", lecturer.Moderates);
                    cmd.Parameters.AddWithValue("@DateOfBirth", lecturer.DateOfBirth);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        // not gonna fix for now  just to update it to soft delete and hard delete 
            public void Delete(int studentNumber)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM student WHERE studentNumber = @studentNumber";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@studentNumber", studentNumber);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }

