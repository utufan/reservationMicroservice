using Microsoft.EntityFrameworkCore;
using dfdsMicroserviceProject.Data;
using Microsoft.OpenApi.Models;
using dfdsMicroserviceProject.Repositories;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Passenger Reservation System APIs", Version = "v1" });
});

var app = builder.Build();

ApplyMigrations(app);


bool enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    if (enableSwagger)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Passenger Reservation System APIs");
            c.RoutePrefix = string.Empty;
        });
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigrations(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ReservationContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Could not migrate.");
        }
    }
}
