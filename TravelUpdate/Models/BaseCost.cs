using System.ComponentModel.DataAnnotations.Schema;

namespace TravelUpdate.Models
{
    public class BaseCost
    {
        public int BaseCostID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal TentativeCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal ActualCost { get; set; }
        
    }
}
