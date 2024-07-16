using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Entities
{
    public class Employer
    {
        public Guid EmployerId { get; set; }
        public Guid UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
