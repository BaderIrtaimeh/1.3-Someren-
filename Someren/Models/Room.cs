namespace Someren.Models
{
    public class Room
    {
        public int RoomNumber { get; set; } //Primary key
        public string BuildingType { get; set; }
        public int NumberOfBeds { get; set; }
        public int FloorLevel { get; set; }

    }
}
