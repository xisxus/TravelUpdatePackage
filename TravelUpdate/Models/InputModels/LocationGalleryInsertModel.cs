namespace TravelUpdate.Models.InputModels
{
    public class LocationGalleryInsertModel
    {
        public IFormFile ImageFile { get; set; }  // For image upload
        public bool IsPrimary { get; set; }
        public string ImageCaption { get; set; }
        public int LocationID { get; set; } // Foreign Key
    }
}
