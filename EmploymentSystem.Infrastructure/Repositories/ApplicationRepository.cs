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
    public class ApplicationRepository : Repository<ApplicationDetails>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ApplicationDetails>> GetApplicationsByApplicantIdAsync(int applicantId)
        {
            return await _context.Applications.Where(a => a.ApplicantId == applicantId).ToListAsync();
        }

        public async Task<bool> HasAppliedTodayAsync(int applicantId)
        {
            var today = DateTime.Today;
            return await _context.Applications.AnyAsync(a => a.ApplicantId == applicantId && a.ApplicationDate >= today);
        }

        public async Task<int> GetApplicationCountByVacancyIdAsync(int vacancyId)
        {
            return await _context.Applications.CountAsync(a => a.VacancyId == vacancyId);
        }
    }

}

