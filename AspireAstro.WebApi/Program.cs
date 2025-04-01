using AspireAstro.WebApi.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<AstroDbContext>(connectionName: "database");
builder.Services.AddCors();

var app = builder.Build();
app.ApplyMigrations();

app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapGet("/api/visitor/{id:int}", async (AstroDbContext context, int id) =>
{
    var blogViews = await context.BlogViews.SingleOrDefaultAsync(bv => bv.BlogId == id);
    if (blogViews is null)
    {
        await context.BlogViews.AddAsync(new BlogViews
        {
            BlogId = id,
            Counter = 1
        });
        await context.SaveChangesAsync();
        return 1;
    }
    else
    {
        blogViews.Counter = blogViews.Counter + 1;
        context.Update(blogViews);
        await context.SaveChangesAsync();
        return blogViews.Counter;
    }
});

app.Run();
