var builder = DistributedApplication.CreateBuilder(args);

var server = builder.AddSqlServer("sqlserver")
    .WithLifetime(ContainerLifetime.Persistent);

var db = server.AddDatabase("database", "astrodb");

var webapi = builder.AddProject<Projects.AspireAstro_WebApi>("webapi")
    .WithReference(db)
    .WaitFor(db);

builder.AddNpmApp("frontend", "../AspireAstro.Frontend")
    .WithHttpEndpoint(env: "PORT")
    .WithReference(webapi);

builder.Build().Run();
