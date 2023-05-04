using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiTest.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using System.Data.Entity;


var builder = WebApplication.CreateBuilder(args);

//adding jwt auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //define which claim requires to check
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //store the value in appsettings.json
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<CuestionarioContext>(opt =>
//opt.UseInMemoryDatabase("Cuestionario"));
builder.Services.AddDbContext<CuestionarioContext>(options =>
    options.UseSqlServer("Data Source=Hakidoto\\SQLEXPRESS;Initial Catalog=db_cuestionario;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

//adding UseAuthentication
app.UseAuthentication();
app.UseAuthorization();
