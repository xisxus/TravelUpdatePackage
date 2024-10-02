namespace TravelUpdate.Models.InputModels
{
    public class StateInsertModel
    {
        public string StateName { get; set; }
        public int CountryID { get; set; } // Foreign Key
    }
}
