using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models.InputModels
{
    public class PackageExcludesUpdateModel
    {
        [Required]
        public int PackageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string ExcludeDescription { get; set; } = " ";
    }

}
