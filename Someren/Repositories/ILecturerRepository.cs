using Someren.Models;

namespace Someren.Repositories
{
    public interface ILecturerRepository
    {
        List<Lecturer> GetAll();
        void Add(Lecturer lecturerID);
        void Delete(int LectureerID);
    }
}
