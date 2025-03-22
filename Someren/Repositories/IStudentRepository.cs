using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;

namespace Someren.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        void Add(Student student);
        void Delete(int studentNumber);
    }

}

