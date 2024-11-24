using Application.Customers.Command;
using Application.Customers.Query;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Bookings.Command;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Agrega el soporte para controladores
builder.Services.AddAuthorization(); // Registra los servicios de autorización

//Autorizacion por token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("La clave JWT no está configurada.")))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                                      .AllowAnyHeader()
                                                      .AllowAnyMethod()));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//context
var connectionString = builder.Configuration.GetConnectionString("Conexion");

builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlServer(connectionString!));

builder.Services.AddMediatR(typeof(CreateCustomerCommand).Assembly);
builder.Services.AddMediatR(typeof(UpdateCustomerCommand).Assembly);
builder.Services.AddMediatR(typeof(GetCustomerListQuery).Assembly);
builder.Services.AddMediatR(typeof(DeleteBookingCommand).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

