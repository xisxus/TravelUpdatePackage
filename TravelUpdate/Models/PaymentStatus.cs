namespace TravelUpdate.Models
{
    public class PaymentStatus
    {
        public int PaymentStatusID { get; set; }
        public string PaymentStatusType { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
