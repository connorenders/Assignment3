using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemesterYear_Assignment3_caenders.Analysis;
using SemesterYear_Assignment3_caenders.Data;
using System.Diagnostics;

namespace SemesterYear_Assignment3_caenders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Testing.DataGen.GenerateReviews("The Dark Knight", 10);
            //Testing.DataGen.GenerateTweets("Margot Robbie", 10);
            //while(true) { }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
