var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var db = postgres.AddDatabase("database", "astrodb");

var webapi = builder.AddProject<Projects.AspireAstro_WebApi>("webapi")
    .WithReference(db)
    .WaitFor(db);

builder.AddNpmApp("frontend", "../AspireAstro.Frontend")
    .WithHttpEndpoint(env: "PORT")
    .WithReference(webapi);

builder.Build().Run();
