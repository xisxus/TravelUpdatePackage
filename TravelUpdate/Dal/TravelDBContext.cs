using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelUpdate.Models;


namespace TravelUpdate.Dal
{
    public class TravelDBContext : IdentityDbContext<ApplicationUser> 
    {
        

        public TravelDBContext(DbContextOptions<TravelDBContext> options) : base(options) { }

        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<BaseCost> BaseCosts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DayWiseTourCost> DayWiseTourCosts { get; set; }
        public virtual DbSet<DayCostCategory> DayCostCategory { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FoodItem> FoodItems { get; set; }        
        public virtual DbSet<Guide> Guides { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelFacility> HotelFacilities { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationGallery> LocationGalleries { get; set; }
        public virtual DbSet<MealType> MealTypes { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageAccommodation> PackageAccommodations { get; set; }
        public virtual DbSet<PackageAccounts> PackageAccounts { get; set; }
        public virtual DbSet<PackageBudget> PackageBudgets { get; set; }
        public virtual DbSet<PackageCategory> PackageCategories { get; set; }
        public virtual DbSet<PackageSubCategory> PackageSubCategories { get; set; }
        public virtual DbSet<PackageDetails> PackageDetails { get; set; }
        public virtual DbSet<PackageIncludes> PackageIncludes { get; set; }
        public virtual DbSet<PackageExcludes> PackageExcludes { get; set; }
        public virtual DbSet<PackageFacility> PackageFacilities { get; set; }
        public virtual DbSet<PackageFAQ> PackageFAQ { get; set; }
        public virtual DbSet<PackageFoodItem> PackageFoodItems { get; set; }
        public virtual DbSet<PackageGallery> PackageGallery { get; set; }
        public virtual DbSet<PackageLocation> PackageLocation { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<PackageTransportation> PackageTransportations { get; set; }
        public virtual DbSet<PackageUser> PackageUsers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; } = default!;
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; } = default!;
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomSubType> RoomSubTypes { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Transportation> Transportations { get; set; }
        public virtual DbSet<TransportationType> TransportationTypes { get; set; }
        public virtual DbSet<TransportProvider> TransportProviders { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionImage> PromotionImages { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<TourVoucher> TourVouchers { get; set; }
        public virtual DbSet<Transportation> Transportation { get; set; }
        public virtual DbSet<TransportationCatagory> TransportationCatagories { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }
        public virtual DbSet<RequestUrl> RequestUrls { get; set; }
        public virtual DbSet<UrlService> UrlServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<RoomType>()
            .HasMany(rt => rt.RoomSubTypes)
            .WithOne(rs => rs.RoomType)
            .HasForeignKey(rs => rs.RoomTypeID)
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomSubType)
                .WithMany(rs => rs.Rooms)
                .HasForeignKey(r => r.RoomSubTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Package>()
            .HasOne(p => p.PackageCategory)
            .WithMany(c => c.Packages)
            .HasForeignKey(p => p.PackageCategoryID);

            modelBuilder.Entity<PackageCategory>()
                .HasMany(c => c.PackageSubCategories)
                .WithOne(sc => sc.PackageCategory)
                .HasForeignKey(sc => sc.PackageCategoryID);


            //modelBuilder.Entity<TourVoucher>()
            //    .HasOne(tv => tv.Package)
            //    .WithMany(p => p.TourVouchers)
            //    .HasForeignKey(tv => tv.PackageID)
            //    .OnDelete(DeleteBehavior.Restrict); 

 
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.TourVoucher)
                .WithMany(tv => tv.Schedules)
                .HasForeignKey(s => s.TourVoucherID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Package)
                .WithMany(p => p.Schedule)  
                .HasForeignKey(s => s.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PackageAccommodation>()
                .HasKey(pa => pa.PackageAccommodationID);

            modelBuilder.Entity<PackageAccommodation>()
                .HasOne(pa => pa.Room)
                .WithMany(h => h.PackageAccommodations)
                .HasForeignKey(pa => pa.RoomID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PackageAccommodation>()
                .HasOne(pa => pa.Package)
                .WithMany(p => p.PackageAccommodations)
                .HasForeignKey(pa => pa.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .Property(r => r.AveragePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DayWiseTourCost>(entity =>
            {
                entity.HasKey(d => d.DayWiseTourCostID);  

    
                entity.HasOne(d => d.DayCostCategory)
                      .WithMany(c => c.DayWiseTourCosts)
                      .HasForeignKey(d => d.DayCostCategoryID)
                      .OnDelete(DeleteBehavior.Cascade); 
            });


            modelBuilder.Entity<DayCostCategory>(entity =>
            {
                entity.HasKey(c => c.DayCostCategoryID);  

                entity.HasMany(c => c.Schedules)
                      .WithOne(s => s.DayCostCategory)
                      .HasForeignKey(s => s.DayCostCategoryID)
                      .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<DayWiseTourCost>()
               .Property(r => r.OtherCost)
               .HasColumnType("decimal(18,2)");

             

            modelBuilder.Entity<Schedule>()
                           .Property(r => r.TentativeCost)
                           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Schedule>()
                           .Property(r => r.ActualCost)
                           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DayWiseTourCost>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalFoodCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalTransPortCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalAccomodationCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalOtherCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalEarn)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalLoss)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalProfit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.EstimateedFoodCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.EstimatedTransportCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.EstimatedAccomodationCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.OtherCost)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.ProfitPercent)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageFoodItem>()
                .Property(r => r.FoodQuantity)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageFoodItem>()
                .Property(r => r.ItemTotalCost)
                .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<Payment>()
                .Property(r => r.FinalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageTransportation>()
                .Property(r => r.PerHeadTransportCost)
                .HasColumnType("decimal(18,2)");

             modelBuilder.Entity<PackageTransportation>()
                .Property(r => r.ItemTransportTotalCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(r => r.AmountPaid)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Promotion>()
                .Property(r => r.DiscountPercentage)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<DayWiseTourCost>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Schedule>(entity =>
            {

                entity.HasOne(s => s.DayCostCategory)
                      .WithMany(d => d.Schedules)
                      .HasForeignKey(s => s.DayCostCategoryID)
                      .OnDelete(DeleteBehavior.Restrict);  
            });

            modelBuilder.Entity<DayCostCategory>(entity =>
            {
                entity.HasKey(d => d.DayCostCategoryID);  
            });


            modelBuilder.Entity<PackageUser>()
                .HasKey(pg => pg.PackageUserID);

            modelBuilder.Entity<PackageUser>()
                .HasOne(pg => pg.Package)
                .WithMany(p => p.PackageUsers)
                .HasForeignKey(pg => pg.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DayWiseTourCost>()
             .HasOne(p => p.Package)
             .WithMany(b => b.DayWiseTourCosts)
             .HasForeignKey(p => p.PackageID)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DayWiseTourCost>()
                .Property(p => p.TotalCost)
                .HasComputedColumnSql("0");


            modelBuilder.Entity<PackageUser>()
                .HasOne(pg => pg.ApplicationUser)
                .WithMany(au => au.PackageUsers)
                .HasForeignKey(pg => pg.ApplicationUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Package)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PackageID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ApplicationUser)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.ApplicationUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Package)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ApplicationUserID)
                .OnDelete(DeleteBehavior.Restrict);



        }
        


    }
}
