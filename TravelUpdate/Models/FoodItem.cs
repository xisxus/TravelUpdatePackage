using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelUpdate.Models
{
    public class FoodItem /*: BaseClass*/
    {
        [Key]
        public int FoodItemID { get; set; } = 1;   
        [Required]
        public string ItemName { get; set; } = "Parota";       
        [Required]
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; }= DateTime.Now;

        public ICollection<PackageFoodItem> PackageMenus { get; set; }= new List<PackageFoodItem>();
    }

}



  