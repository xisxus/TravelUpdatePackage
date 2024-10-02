namespace TravelUpdate.Models.InputModels
{
    public class TourVoucherInsertModel
    {
        public string TourVoucherCode { get; set; } = "";
        public IFormFile? VoucherFile { get; set; }

        public int PackageID { get; set; }
    }

}
