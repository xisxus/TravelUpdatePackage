using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models.InputModels
{
    public class RoomTypeUpdateModel
    {
        public int RoomTypeID { get; set; } // Include RoomTypeID for updates

        [Required]
        public string TypeName { get; set; }
    }

}
