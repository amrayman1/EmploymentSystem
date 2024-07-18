using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Repositories
{
    public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
    {
        private readonly ILogger<VacancyRepository> _logger;
        private readonly ApplicationDbContext _context;

        public VacancyRepository(ApplicationDbContext context, ILogger<VacancyRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesAsync(string employerId)
        {
            _logger.LogInformation("Fetching vacancies for employer with ID: {EmployerId}", employerId);

            try
            {
                return await _context.Vacancies
                    .Where(v => v.EmployerId == employerId && v.ExpiryDate > DateTime.Now)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vacancies for employer with ID: {EmployerId}", employerId);
                throw;
            }
        }

        public async Task<IEnumerable<Vacancy>> GetActiveVacanciesAsync()
        {
            _logger.LogInformation("Fetching active vacancies");

            try
            {
                return await _context.Vacancies
                    .Where(v => v.IsActive && v.ExpiryDate > DateTime.Now)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching active vacancies");
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationDetails>> GetApplicationsByVacancyIdAsync(int vacancyId)
        {
            _logger.LogInformation("Fetching applications for vacancy with ID: {VacancyId}", vacancyId);

            try
            {
                return await _context.Applications
                    .Where(a => a.VacancyId == vacancyId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching applications for vacancy with ID: {VacancyId}", vacancyId);
                throw;
            }
        }

        public async Task<IEnumerable<Vacancy>> SearchVacanciesAsync(string title)
        {
            _logger.LogInformation("Searching vacancies with title containing: {Title}", title);

            try
            {
                var query = _context.Vacancies.AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    var lowerTitle = title.ToLower();
                    query = query.Where(v => v.Title.ToLower().Contains(lowerTitle) && v.IsActive);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching vacancies with title containing: {Title}", title);
                throw;
            }
        }

        public async Task ArchiveExpiredVacanciesAsync()
        {
            _logger.LogInformation("Archiving expired vacancies");

            try
            {
                var expiredVacancies = await _context.Vacancies
                    .Where(v => v.ExpiryDate < DateTime.Now && !v.IsArchived)
                    .ToListAsync();

                foreach (var vacancy in expiredVacancies)
                {
                    vacancy.IsActive = false;
                    vacancy.IsArchived = true;
                }

                _context.Vacancies.UpdateRange(expiredVacancies);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while archiving expired vacancies");
                throw;
            }
        }

    }

}
