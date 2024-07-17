using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Repositories
{
    public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Vacancy>> GetVacanciesAsync(string employerId)
        {
            return await _context.Vacancies.Where(v => v.EmployerId == employerId && v.ExpiryDate > DateTime.Now).ToListAsync();
        }
        
        public async Task<IEnumerable<Vacancy>> GetActiveVacanciesAsync()
        {
            return await _context.Vacancies.Where(v => v.IsActive && v.ExpiryDate > DateTime.Now).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationDetails>> GetApplicationsByVacancyIdAsync(int vacancyId)
        {
            return await _context.Applications.Where(a => a.VacancyId == vacancyId).ToListAsync();
        }

        public async Task<IEnumerable<Vacancy>> SearchVacanciesAsync(string title)
        {
            var query = _context.Vacancies.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(v => v.Title.Contains(title, StringComparison.OrdinalIgnoreCase) && v.IsActive == true);
            }
            return await Task.FromResult(query.ToList());
        }

    }

}
