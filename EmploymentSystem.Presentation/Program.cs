using EmploymentSystem.Application.Commands;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using EmploymentSystem.Infrastructure.Repositories;
using EmploymentSystem.Presentation.Helper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Starting
builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection String not found!"));
});

// Add Identity and JWT Authentication
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();



//JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employment System API", Version = "v1" });
});



//builder.Services.AddMediatR(typeof(LoginUserCommandHandler).Assembly);
//builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
//builder.Services.AddMediatR(typeof(ApplyForVacancyCommandHandler).Assembly);
//builder.Services.AddMediatR(typeof(CreateVacancyCommandHandler).Assembly);


// Register your handlers
builder.Services.AddTransient<IRequestHandler<RegisterUserCommand, IdentityResult>, RegisterUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<LoginUserCommand, string>, LoginUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<CreateVacancyCommand, Vacancy>, CreateVacancyCommandHandler>();
builder.Services.AddTransient<IRequestHandler<ApplyForVacancyCommand, ApplicationDetails>, ApplyForVacancyCommandHandler>();


builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
