using Microsoft.EntityFrameworkCore;
using WebApp.Ui.Data;

namespace WebApp.Ui.Seed;

public static class MigrationHelper
{
    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        // Initialise and seed database
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.SeedAsync();
    }

    public static bool DatabaseExist(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString, options =>
        {
            options.EnableRetryOnFailure();
            options.CommandTimeout((int)TimeSpan.FromSeconds(30).TotalSeconds);
        });

        var db = new ApplicationDbContext(optionsBuilder.Options).Database.CanConnect();
        return db;
    }
}