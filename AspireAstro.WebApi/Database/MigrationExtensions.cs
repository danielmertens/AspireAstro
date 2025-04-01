using Microsoft.EntityFrameworkCore;

namespace AspireAstro.WebApi.Database;

public static class MigrationExtensions
{
    public const int MAX_RETRIES = 3;
    public const int DELAY = 3000;

    public static async Task ApplyMigrations(this WebApplication app)
    {
        var attempt = 1;
        while (attempt <= MAX_RETRIES)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AstroDbContext>();
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    Console.WriteLine("Applying pending migrations...");
                    await dbContext.Database.MigrateAsync();
                    Console.WriteLine("Migrations applied successfully.");
                }
                else
                {
                    Console.WriteLine("No pending migrations found.");
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                if (attempt == MAX_RETRIES)
                {
                    throw;
                }

                await Task.Delay(DELAY);
            }

            attempt++;
        }
    }
}
