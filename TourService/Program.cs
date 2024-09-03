using Microsoft.EntityFrameworkCore;
using TourService.Data;
using TourService.Extensions;
using TourService.Services;
using TourService.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//connect to DB

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

//Register services for DI

builder.Services.AddScoped<IImage,TourImageService>();
builder.Services.AddScoped<ITour,ToursService>();


//register automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//Custom Services-Extension Folder
builder.AddAuth();
builder.AddSwaggenGenExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
