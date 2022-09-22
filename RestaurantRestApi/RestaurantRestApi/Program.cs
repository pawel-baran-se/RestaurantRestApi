using Microsoft.EntityFrameworkCore;
using NLog.Web;
using RestaurantRestApi;
using RestaurantRestApi.DbConfigurations;
using RestaurantRestApi.Entities;
using RestaurantRestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RestaurantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDb"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
