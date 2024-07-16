using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using EmploymentSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Presentation.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EmploymentDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployerRepository, EmployerRepository>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IVacancyRepository, VacancyRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();

            return services;
        }
    }
}
