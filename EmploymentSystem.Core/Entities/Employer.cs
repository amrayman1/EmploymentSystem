using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Entities
{
    public class Employer : User
    {
        public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
    }
}
