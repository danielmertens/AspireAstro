using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspireAstro.WebApi.Database;

public class AstroDbContext : DbContext
{
    public DbSet<BlogViews> BlogViews { get; set; }
    public AstroDbContext(DbContextOptions<AstroDbContext> options)
            : base(options)
    {
    }
}

public class BlogViews
{
    public Guid Id { get; set; }
    public int BlogId { get; set; }
    public int Counter { get; set; }
}
