using EmploymentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Core.Interfaces
{
    public interface IVacancyRepository : IRepository<Vacancy>
    {
        Task<IEnumerable<Vacancy>> GetActiveVacanciesAsync();
        Task<IEnumerable<ApplicationDetails>> GetApplicationsByVacancyIdAsync(int vacancyId);
        Task<IEnumerable<Vacancy>> GetVacanciesAsync(string employerId);
        Task<IEnumerable<Vacancy>> SearchVacanciesAsync(string title);
        Task ArchiveExpiredVacanciesAsync();
    }
}
