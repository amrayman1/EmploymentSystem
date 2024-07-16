using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Core.Entities
{
    public class Applicant : User
    {
        public List<ApplicationDetails> Applications { get; set; } = new List<ApplicationDetails>();
    }
}
