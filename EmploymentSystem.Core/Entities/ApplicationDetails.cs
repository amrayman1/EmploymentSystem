using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Entities
{
    public class ApplicationDetails
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; }

        [ForeignKey("VacancyId")]
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public string ApplicantId { get; set; }
    }
}
