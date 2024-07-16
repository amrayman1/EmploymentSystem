using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Entities
{
    public class ApplicationDetails
    {
        public Guid ApplicationId { get; set; }
        public Guid VacancyId { get; set; }
        public Guid ApplicantId { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
