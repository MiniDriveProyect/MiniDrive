using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MiniDrive.Data;
using CouponApi.Extensions;
//using MiniDrive.Services.Implementations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Database configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
                   Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"),
                   mySqlOptions => mySqlOptions.EnableRetryOnFailure())
   );

// Repositories scopes
builder.Services.AddRepositories(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();