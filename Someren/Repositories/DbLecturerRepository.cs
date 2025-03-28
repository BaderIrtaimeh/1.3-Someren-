using Microsoft.Data.SqlClient;
    using Microsoft.Data.SqlClient;
    using Someren.Models;
    using System.Collections.Generic;

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
            var lecturers = new List<Lecturer>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
               
                string query = @"
                SELECT 
                    LecturerID, 
                    FirstName, 
                    LastName, 
                    PhoneNumber, 
                    DateOfBirth
                FROM Lecturer
            ";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lecturers.Add(new Lecturer
                    {
                        
                        LectureID = reader.GetInt32(reader.GetOrdinal("LecturerID")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"))
                    });
                }
            }

            return lecturers;
        }

        
        public Lecturer GetById(int id)
        {
            Lecturer lecturer = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
              
                string query = @"
                SELECT 
                    LecturerID, 
                    FirstName, 
                    LastName, 
                    PhoneNumber, 
                    DateOfBirth
                FROM Lecturer
                WHERE LecturerID = @Id
            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lecturer = new Lecturer
                    {
                        LectureID = reader.GetInt32(reader.GetOrdinal("LecturerID")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"))
                    };
                }
            }

            return lecturer;
        }

       
        public void Add(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                INSERT INTO Lecturer (FirstName, LastName, PhoneNumber, DateOfBirth) 
                VALUES (@FirstName, @LastName, @PhoneNumber, @DateOfBirth)
            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", lecturer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", lecturer.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", lecturer.DateOfBirth);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

      
        public void Update(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                UPDATE Lecturer 
                SET FirstName   = @FirstName, 
                    LastName    = @LastName, 
                    PhoneNumber = @PhoneNumber, 
                    DateOfBirth = @DateOfBirth
                WHERE LecturerID = @LecturerID
            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", lecturer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", lecturer.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", lecturer.DateOfBirth);

               
                cmd.Parameters.AddWithValue("@LecturerID", lecturer.LectureID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

       
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Lecturer WHERE LecturerID = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}



