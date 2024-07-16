using EmploymentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Core.Interfaces
{
    public interface IApplicationRepository : IRepository<ApplicationDetails>
    {
        Task<IEnumerable<ApplicationDetails>> GetApplicationsByApplicantIdAsync(int applicantId);
        Task<bool> HasAppliedTodayAsync(int applicantId);
        Task<int> GetApplicationCountByVacancyIdAsync(int vacancyId);
    }
}
