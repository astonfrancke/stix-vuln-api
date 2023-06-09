using Microsoft.EntityFrameworkCore;

using StixVuln.Api;
using StixVuln.Infrastructure;
using StixVuln.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedDatabase.Initialize(services);
    }
    catch (Exception ex)
    {
        // TODO: Log error
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();

public partial class Program { }
