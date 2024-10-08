using EmploymentSystem.Application.Commands.Accounts.Login;
using EmploymentSystem.Application.Commands.Accounts.RegisterAsApplicant;
using EmploymentSystem.Application.Commands.Accounts.RegisterAsEmployer;
using EmploymentSystem.Application.Commands.Vacancies.ApplyForVacancy;
using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using EmploymentSystem.Application.Commands.Vacancies.GetAllApplicants;
using EmploymentSystem.Application.Commands.Vacancies.GetAllVacancies;
using EmploymentSystem.Application.Features.Accounts.Login;
using EmploymentSystem.Application.Features.Accounts.RegisterAsApplicant;
using EmploymentSystem.Application.Features.Accounts.RegisterAsEmployer;
using EmploymentSystem.Application.Features.Vacancies.ApplyForVacancy;
using EmploymentSystem.Application.Features.Vacancies.CreateVacancy;
using EmploymentSystem.Application.Features.Vacancies.DeleteVacancy;
using EmploymentSystem.Application.Features.Vacancies.EditVacancy;
using EmploymentSystem.Application.Features.Vacancies.GatAllVacancies;
using EmploymentSystem.Application.Features.Vacancies.GetActiveVacancies;
using EmploymentSystem.Application.Features.Vacancies.GetApplicants;
using EmploymentSystem.Application.Features.Vacancies.SearchForVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using EmploymentSystem.Infrastructure.Repositories;
using EmploymentSystem.Infrastructure.Services;
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

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


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


// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

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
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
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

});


// Register your handlers
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddTransient<IRequestHandler<RegisterAsEmployerCommand, IdentityResult>, RegisterAsEmployerCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RegisterAsApplicantCommand, IdentityResult>, RegisterAsApplicantCommandHandler>();
builder.Services.AddTransient<IRequestHandler<LoginUserCommand, string>, LoginUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<CreateVacancyCommand, Vacancy>, CreateVacancyCommandHandler>();
builder.Services.AddTransient<IRequestHandler<EditVacancyCommand, Vacancy>, EditVacancyCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteVacancyCommand, bool>, DeleteVacancyCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetApplicantsForVacancyQuery, IEnumerable<ApplicationDetails>>, GetApplicantsForVacancyQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetVacanciesQuery, IEnumerable<Vacancy>>, GetVacanciesQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetActiveVacanciesQuery, IEnumerable<Vacancy>>, GetActiveVacanciesQueryHandler>();
builder.Services.AddTransient<IRequestHandler<ApplyForVacancyCommand, ApplicationDetails>, ApplyForVacancyCommandHandler>();
builder.Services.AddTransient<IRequestHandler<SearchVacanciesQuery, IEnumerable<Vacancy>>, SearchVacanciesQueryHandler>();


// Register background service
builder.Services.AddHostedService<VacancyArchivingService>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


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
