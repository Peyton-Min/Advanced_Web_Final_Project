using Event_Manager_Final_Project_Advanced_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace Event_Manager_Final_Project_Advanced_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUserRepository, DbUserRepository>();
            builder.Services.AddScoped<IEventRepository, DbEventRepository>();
            builder.Services.AddScoped<IEventParticipantRepository, DbEventParticipantRepository>();
            builder.Services.AddScoped<Initializer>();

            var app = builder.Build();

            await SeedDataAsync(app);

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

            app.Run();

            static async Task SeedDataAsync(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                try
                {
                    var initializer = services.GetRequiredService<Initializer>();
                    await initializer.SeedDatabaseAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError("An error occurred while seeding the database: {Message}", ex.Message);
                }
            }
        }
    }
}
