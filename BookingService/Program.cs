using BookingService.Data;
using BookingService.Extensions;
using BookingService.Services;
using BookingService.Services.Iservices;
using Microsoft.EntityFrameworkCore;
using SafariMessageBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//add the swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAuth();
builder.AddSwaggenGenExtension();


//dependency injection for the services
builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<ITour, TourService>();
builder.Services.AddScoped<IBooking, BookingsService>();
builder.Services.AddScoped<ICoupon, CouponService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();

//add the http client for making requests to other services
builder.Services.AddHttpClient("Tours", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:TourService")));
builder.Services.AddHttpClient("Coupons", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CouponService")));
builder.Services.AddHttpClient("Hotels", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:HotelService")));
builder.Services.AddHttpClient("Users", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:UserService")));



builder.Services.AddCors(options =>
{
    options.AddPolicy("BookingPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:7000");
        // Optionally allow specific methods (GET, POST, etc.)
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
        // Optionally allow specific headers
        policy.WithHeaders("Content-Type", "Authorization");
    });

});

var app = builder.Build();


Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();

app.UseCors("BookingPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
