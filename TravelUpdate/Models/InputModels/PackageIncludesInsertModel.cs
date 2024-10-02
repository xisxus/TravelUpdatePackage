using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models.InputModels
{
    public class PackageIncludesInsertModel
    {
        [Required]
        public int PackageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string IncludeDescription { get; set; } = " ";
    }

}
