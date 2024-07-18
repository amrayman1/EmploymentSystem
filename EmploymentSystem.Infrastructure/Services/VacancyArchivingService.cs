using EmploymentSystem.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Services
{
    public class VacancyArchivingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<VacancyArchivingService> _logger;

        public VacancyArchivingService(IServiceProvider serviceProvider, ILogger<VacancyArchivingService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var vacancyRepository = scope.ServiceProvider.GetRequiredService<IVacancyRepository>();
                        await vacancyRepository.ArchiveExpiredVacanciesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while archiving expired vacancies.");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
