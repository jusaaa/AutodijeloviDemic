using AutodijeloviDemic.Data;
using AutodijeloviDemic.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace AutodijeloviDemic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Configure Entity Framework with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add Identity services
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Ako ne tražiš potvrdu email-a
                options.Password.RequiredLength = 6; // Minimalna dužina lozinke
                options.Password.RequireNonAlphanumeric = false; // Bez posebnih znakova
                options.Password.RequireDigit = true; // Obavezna cifra
                options.Password.RequireUppercase = true; // Obavezno veliko slovo
            })
             .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add cookie authentication
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login"; // Putanja za ugrađenu prijavu
                options.LogoutPath = "/Identity/Account/Logout"; // Putanja za ugrađenu odjavu
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Trajanje sesije
                options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Stranica za zabranu pristupa
            });

            // Add MVC services
            builder.Services.AddControllersWithViews();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Autodijelovi API",
                    Version = "v1",
                    Description = "API for managing car parts in AutodijeloviDemic"
                });
            });

            // Add session services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Set Bosnian culture globally
            var bosnianCulture = new CultureInfo("bs-BA");
            CultureInfo.DefaultThreadCurrentCulture = bosnianCulture;
            CultureInfo.DefaultThreadCurrentUICulture = bosnianCulture;

            var app = builder.Build();

            // Apply migrations at runtime
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate(); // Automatically apply migrations
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();

                // Enable Swagger in development
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Autodijelovi API v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Add authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession(); // Middleware za sesije

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map Identity Razor Pages
            app.MapRazorPages();

            app.Run();
        }
    }
}
