namespace Someren.Models
{
    public class Activity
    {
        public int ActivityID { get; set; } //Primary Key
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
    }
}
