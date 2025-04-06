using Someren.Models;

namespace Someren.Repositories
{
    public interface ILecturerRepository
    {
        List<Lecturer> GetAll();
        Lecturer GetById(int id);
        void Add(Lecturer lecturer);
        void Update(Lecturer lecturer);
        void Delete(int id);
        List<Lecturer> GetParticipatingLecturers(int activityID);
        List<Lecturer> GetAvailableLecturers();

        public void AssignLecturerToActivity(int lecturerId, int activityId);
        public void RemoveLecturerFromActivity(int lecturerId);

    }
}
