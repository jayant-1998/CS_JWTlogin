using JWTLogin.DAL.DBContexts;
using JWTLogin.DAL.Repositories.Implementations;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Services.Implementations;
using JWTLogin.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJWTEmployeeRepository, JWTEmployeeRepository>();
builder.Services.AddTransient<IJWTEmployeeService, JWTEmployeeService>();

builder.Services.AddDbContext<JWTEmployeeDbContext>(options => options.UseSqlServer("name=DefaultConnection"));

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
