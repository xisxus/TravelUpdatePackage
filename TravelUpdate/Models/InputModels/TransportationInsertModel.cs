using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models.InputModels
{
    public class TransportationInsertModel
    {
        public bool IsActive { get; set; } = true; // Default value
        public int TransportProviderID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }

}
