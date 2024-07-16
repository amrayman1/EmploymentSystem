using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Infrastructure.Data
{
    public class EmploymentDbContext : DbContext
    {
        public EmploymentDbContext(DbContextOptions<EmploymentDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<ApplicationDetails> Applications { get; set; }
    }
}
