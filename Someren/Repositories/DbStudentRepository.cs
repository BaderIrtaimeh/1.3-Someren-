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
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM student";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentNumber = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Class = reader.GetString(4)
                    });
                }
            }
            return students;
        }
        public void Add(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO student (studentNumber, firstName, lastName, telephoneNumber, class)
                         VALUES (@studentNumber, @firstName, @lastName, @telephoneNumber, @class)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentNumber", student.StudentNumber);
                cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                cmd.Parameters.AddWithValue("@lastName", student.LastName);
                cmd.Parameters.AddWithValue("@telephoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@class", student.Class);          

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

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


