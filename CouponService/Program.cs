using CouponService.Data;
using CouponService.Extensions;
using CouponService.Services;
using CouponService.Services.Iservices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configure Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});
//register service for DI
builder.Services.AddScoped<ICoupon, CouponsService>();

//Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddCors(options =>
{
    options.AddPolicy("CouponPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:7000");
        // Optionally allow specific methods (GET, POST, etc.)
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
        // Optionally allow specific headers
        policy.WithHeaders("Content-Type", "Authorization");
    });
});



//custom services
builder.AddAuth();
builder.AddSwaggenGenExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");


app.UseMigrations();

app.UseHttpsRedirection();

app.UseCors("CouponPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
