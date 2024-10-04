// Program.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using TravelUpdate.Dal;
using TravelUpdate.Models;

namespace TravelUpdate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TravelDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TravelDBContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options (optional)
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                // Lockout settings (optional)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings (optional)
                options.User.RequireUniqueEmail = true;
            });

            //// Add CORS services
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin", builder =>
            //    {
            //        builder.WithOrigins("http://localhost:4200") // Angular app URL
            //               .AllowAnyMethod()
            //               .AllowAnyHeader();
            //    });
            //});

            // Add CORS services with any origin allowed
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()    // Allow requests from any origin
                           .AllowAnyMethod()    // Allow any HTTP method (GET, POST, etc.)
                           .AllowAnyHeader();   // Allow any headers
                });
            });




            // Add authentication and authorization middleware
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Use CORS middleware
           // app.UseCors("AllowSpecificOrigin");

            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();


         


            app.UseAuthentication(); 
            app.UseAuthorization();   

            app.MapControllers();
            app.Run();
        }
    }
}
