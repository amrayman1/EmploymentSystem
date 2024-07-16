using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Entities
{
    public class Applicant
    {
        public Guid ApplicantId { get; set; }
        public Guid UserId { get; set; }
        public string Resume { get; set; }
    }
}
