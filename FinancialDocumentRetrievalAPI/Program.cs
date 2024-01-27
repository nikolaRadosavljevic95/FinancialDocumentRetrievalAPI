using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using Infrastructure;
using FastEndpoints.Swagger;
using Serilog;
using Serilog.Formatting.Compact;
using Domain.Common;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Seeding;
using System;
using Autofac.Core;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(hostContext.Configuration)
        .WriteTo.Console()
        .WriteTo.File(new CompactJsonFormatter(), "Logs/Log-.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
        .Enrich.FromLogContext();
});

// Setup for API documentation and exploration
builder.Services.AddEndpointsApiExplorer();

string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
Guard.AgainstNull(connectionString, nameof(connectionString));
builder.Services.AddApplicationDbContext(connectionString);

// FastEndpoints and Swagger configuration
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
});

// Configure Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
           .ConfigureContainer<ContainerBuilder>(containerBuilder =>
           {
               // Register your modules or services here
               containerBuilder.RegisterModule(new AutofacInfrastructureModule());
           });

var app = builder.Build();

// Apply Serilog request logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use developer exception page for detailed error information in development
    app.UseDeveloperExceptionPage();
}
else
{
    // Use default exception handler and HTTP Strict Transport Security in production
    app.UseDefaultExceptionHandler(); // From FastEndpoints
    app.UseHsts();
}

app.UseFastEndpoints();
app.UseSwaggerGen(); // FastEndpoints middleware

app.UseHttpsRedirection();

SeedDatabase(app);

app.Run();

static void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
        SeedData.Initialize(dbContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
