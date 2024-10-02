namespace TravelUpdate.Models.InputModels
{
    public class PackageGalleryInsertModel
    {
        public IFormFile ImageFile { get; set; }
        public bool IsPrimary { get; set; }
        public string ImageCaption { get; set; }

        public int PackageID { get; set; }
    }
}
