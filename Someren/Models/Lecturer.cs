namespace Someren.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; } //primary jey
        public string Name { get; set; }
        public bool Moderates { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
