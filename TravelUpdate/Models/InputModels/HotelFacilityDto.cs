namespace TravelUpdate.Models.InputModels
{
    public class HotelFacilityDto
    {
        public int HotelFacilityId { get; set; } // Optional for updates
        public int HotelId { get; set; } // Foreign key to Hotel
        public int FacilityID { get; set; } // The ID of the facility (FK)
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
