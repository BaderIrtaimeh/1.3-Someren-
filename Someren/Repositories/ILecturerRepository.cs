using Someren.Models;

namespace Someren.Repositories
{
    public interface ILecturerRepository
    {
        List<Lecturer> GetAll();
        Lecturer GetById(string id);
        void Add(Lecturer lecturer);
        void Update(Lecturer lecturer);
        void Delete(string id);

    }
}
