﻿namespace TravelUpdate.Models
{
    public class TourVoucher
    {
        public int TourVoucherID { get; set; }
        public string TourVoucherCode { get; set; } = "";
        public string? VoucherUrl {  get; set; }
        public int PackageID { get; set; } = 1;
        public Package? Package { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }=new List<Schedule>();
    }
}
