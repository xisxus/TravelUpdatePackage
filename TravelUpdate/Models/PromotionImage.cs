namespace TravelUpdate.Models
{
    public class PromotionImage
    {
        public int PromotionImageID { get; set; }
        public string PromotionImageUrl { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}
