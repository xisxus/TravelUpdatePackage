using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models.InputModels
{
    public class RoomTypeInsertModel
    {
        [Required]
        public string TypeName { get; set; }
    }

}
