namespace TravelUpdate.Models
{
    public class RoomSubType
    {
        public int RoomSubTypeID { get; set; }
        public string SubTypeName { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
