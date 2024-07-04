using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MiniDrive.Data;
using MiniDrive.Utils;
using CouponApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MiniDrive.Services.MailerSend;
using MiniDrive.Models;

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

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repositories scopes
builder.Services.AddRepositories(Assembly.GetExecutingAssembly());

//Configuration  token JWT
builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
        options => {
             options.TokenValidationParameters = new TokenValidationParameters{

                //parametros de validacion del token
                ValidateIssuer  = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

             };
        }
    );

//registo de email service
builder.Services.AddHttpClient<IEmailService, EmailService>();
builder.Services.Configure<MailerSendOptions>(builder.Configuration.GetSection("MailerSend"));

//cors 
builder.Services.AddCors(options=> {
    options.AddPolicy("Policy", n => { 
        n.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//polica del cors!
app.UseCors("Policy");
app.MapControllers();
app.UseHttpsRedirection();

app.Run();