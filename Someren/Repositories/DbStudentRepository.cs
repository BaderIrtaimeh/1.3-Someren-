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
                string query = "SELECT * FROM student"; // must change the * to column names.
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentNumber = reader.GetString(),
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE StudentNumber = @studentNumber", conn); // must change the * to column names.
                cmd.Parameters.AddWithValue("@studentNumber", studentNumber);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    student = new Student
                    {
                        StudentNumber = reader["StudentNumber"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Class = reader["Class"].ToString()
                    };
                }
            }

            return student;
        }

        public void Add(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO student (studentNumber, firstName, lastName, phoneNumber, class)
                VALUES (@studentNumber, @firstName, @lastName, @phoneNumber, @class)";


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentNumber", student.StudentNumber);
                cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                cmd.Parameters.AddWithValue("@lastName", student.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@class", student.Class);          

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string studentNumber)
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

        public void Update(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            UPDATE Student 
            SET FirstName = @firstName, LastName = @lastName, PhoneNumber = @phoneNumber, Class = @class 
            WHERE StudentNumber = @studentNumber", conn);

                cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                cmd.Parameters.AddWithValue("@lastName", student.LastName);
                cmd.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@class", student.Class);
                cmd.Parameters.AddWithValue("@studentNumber", student.StudentNumber);

                cmd.ExecuteNonQuery();
            }
        }


    }


}


