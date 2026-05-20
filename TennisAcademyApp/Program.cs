using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Services.Core;
using TennisAcademyApp.Services.Core.Contracts;
using static TennisAcademyApp.Data.Seeding.RoleSeeding;

namespace TennisAcademyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<TennisAcademyDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TennisAcademyDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews(cfg =>
            {
                cfg.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddScoped<ICoachService, CoachService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<ISurfaceService, SurfaceService>();
            builder.Services.AddScoped<ITrainingTypeService, TrainingTypeService>();
            builder.Services.AddScoped<IRacketService, RacketService>();
            builder.Services.AddScoped<IRacketCartService, RacketCartService>();
            builder.Services.AddScoped<IFavouriteCoachService, FavouriteCoachService>();
            builder.Services.AddScoped<IBallService, BallService>();
            builder.Services.AddScoped<IBallCartService, BallCartService>();
            builder.Services.AddScoped<IBagService, BagService>();
            builder.Services.AddScoped<IBagCartService, BagCartService>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build(); // <--- Container is locked here

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedIdentityAsync(services).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure Localization Middleware
            var supportedCultures = new[] { "en", "bg" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}