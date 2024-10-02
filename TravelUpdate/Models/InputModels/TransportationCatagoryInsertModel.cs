using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models.InputModels
{
    public class TransportationCatagoryInsertModel
    {
        [Required]
        [StringLength(100)]
        public string TransportationCatagoryName { get; set; }
    }

}
