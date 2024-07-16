using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Core.Entities
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int MaxApplications { get; set; }

        [ForeignKey("EmployerId")]
        public Employer Employer { get; set; }
        public string EmployerId { get; set; }

        public List<ApplicationDetails> Applications { get; set; } = new List<ApplicationDetails>();
    }
}
