using EmploymentSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Presentation.Helper
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
