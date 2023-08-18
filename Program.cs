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


//builder.Services.AddAuthentication(t =>
//{
//    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    t.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

//})
//    .AddJwtBearer(option =>
//    {
//        option.SaveToken = true;
//        option.RequireHttpsMetadata = false;
//        option.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ValidIssuer = builder.Configuration["Jwt:ValidAudience"],
//            ValidAudience = builder.Configuration["Jwt:ValidIssuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
//        };
//    });

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name=DefaultConnection"));

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
