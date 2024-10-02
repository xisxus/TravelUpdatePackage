namespace TravelUpdate.Models
{
    public class TransportationCatagory
    {
        public int TransportationCatagoryID { get; set; }
        public string TransportationCatagoryName { get; set; }
        public ICollection<PackageTransportation> PackageTransportations { get; set; }
    }

}
