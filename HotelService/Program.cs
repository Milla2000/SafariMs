using HotelService.Data;
using HotelService.Extensions;
using HotelService.Services;
using HotelService.Services.Iservices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

builder.AddAuth();
builder.AddSwaggenGenExtension();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());





builder.Logging.AddConsole();







builder.Services.AddHttpClient("Tours", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:TourService")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("HotelPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        // Optionally allow specific methods (GET, POST, etc.)
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
        // Optionally allow specific headers
        policy.WithHeaders("Content-Type", "Authorization");
    });
});

//var serviceUri = builder.Configuration.GetValue<string>("ServiceURl:TourService");
//Console.WriteLine($"ServiceURl:TourService: {serviceUri}");

//if (string.IsNullOrEmpty(serviceUri))
//{
//    // Log error and stop the application
//    Console.WriteLine("Error: ServiceURl:TourService configuration is missing.");
//    Environment.Exit(1); // Exit with an error code
//}

//this is registering the service used in the controller for dependency injection
builder.Services.AddScoped<ITour, ToursServvices>();
builder.Services.AddScoped<IHotel, HotelsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("HotelPolicy");

app.UseMigrations();

app.UseAuthorization();

app.MapControllers();

app.Run();
