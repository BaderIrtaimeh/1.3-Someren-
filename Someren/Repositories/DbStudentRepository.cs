using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Someren.Models;

namespace Someren.Repositories
{
    public class DbStudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public DbStudentRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SomerenDatabase");
        }

        public List<Student> GetAll()
        {
            var students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT 
                    StudentNumber,
                    FirstName,
                    LastName,
                    PhoneNumber,
                    [Class]
                FROM Student
            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentNumber = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Class = reader.GetString(4)
                    });
                }
            }
            return students;
        }

        public Student GetById(string studentNumber)
        {
            Student student = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    StudentNumber,
                    FirstName,
                    LastName,
                    PhoneNumber,
                    [Class]
                FROM Student 
                WHERE StudentNumber = @studentNumber
            ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentNumber", studentNumber);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        StudentNumber = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Class = reader.GetString(4)
                    };
                }
            }
            return student;
        }

        public void Add(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                INSERT INTO Student (StudentNumber, FirstName, LastName, PhoneNumber, [Class])
                VALUES (@StudentNumber, @FirstName, @LastName, @PhoneNumber, @Class);
            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@Class", student.Class);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                UPDATE Student
                SET FirstName   = @FirstName,
                    LastName    = @LastName,
                    PhoneNumber = @PhoneNumber,
                    [Class]     = @Class
                WHERE StudentNumber = @StudentNumber
            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@Class", student.Class);
                cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string studentNumber)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Student WHERE StudentNumber = @StudentNumber";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentNumber", studentNumber);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}


