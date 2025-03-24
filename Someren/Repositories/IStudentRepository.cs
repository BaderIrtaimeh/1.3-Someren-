using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Someren.Models;

namespace Someren.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(string studentNumber); 
        void Add(Student student);
        void Update(Student student);          
        void Delete(string studentNumber);     
    }

}

