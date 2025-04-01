using Microsoft.EntityFrameworkCore;

namespace AspireAstro.WebApi.Database;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AstroDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            //// Check and apply pending migrations
            //var pendingMigrations = dbContext.Database.GetPendingMigrations();
            //if (pendingMigrations.Any())
            //{
            //    Console.WriteLine("Applying pending migrations...");
            //    Console.WriteLine("Migrations applied successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("No pending migrations found.");
            //}
        }
    }
}
