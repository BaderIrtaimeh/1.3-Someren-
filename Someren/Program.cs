using Someren.Models;
using Someren.Repositories;

namespace Someren
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ILecturerRepository, DbLecturerRepository>();
            builder.Services.AddScoped<IStudentRepository, DbStudentRepository>();
            builder.Services.AddScoped<IActivityRepository, DbActivityRepository>();


            var connectionString = builder.Configuration.GetConnectionString("SomerenDatabase");


            builder.Services.AddScoped<IDrinkRepository>(sp => new DbDrinkRepository(connectionString));
            builder.Services.AddScoped<IDrinkOrderRepository>(sp => new DbDrinkOrderRepository(connectionString));




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

          



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "students",
                pattern: "{controller=Students}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
