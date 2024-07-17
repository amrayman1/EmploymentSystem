using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Repositories
{
    public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Vacancy>> GetVacanciesAsync(string employerId)
        {
            return await _context.Vacancies
                .Where(v => v.EmployerId == employerId && v.ExpiryDate > DateTime.Now)
                .Include(v => v.Applications)
                .ThenInclude(a => a.Applicant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vacancy>> GetActiveVacanciesAsync()
        {
            return await _context.Vacancies
                .Where(v => v.IsActive && v.ExpiryDate > DateTime.Now)
                .Include(v => v.Employer)
                .Include(v => v.Applications)
                .ThenInclude(a => a.Applicant)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationDetails>> GetApplicationsByVacancyIdAsync(int vacancyId)
        {
            return await _context.Applications
                .Where(a => a.VacancyId == vacancyId)
                .Include(a => a.Applicant)
                .Include(a => a.Vacancy)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vacancy>> SearchVacanciesAsync(string title)
        {
            var query = _context.Vacancies.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                title = title.ToLower();
                query = query.Where(v => v.Title.ToLower().Contains(title) && v.IsActive == true);
            }

            return await query
                .Include(v => v.Employer)
                .Include(v => v.Applications)
                .ThenInclude(a => a.Applicant)
                .ToListAsync();
        }
    }
}
