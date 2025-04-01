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
                await dbContext.Database.EnsureCreatedAsync();
                await dbContext.Database.MigrateAsync();
                return;
            }
            catch (Exception)
            {
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
