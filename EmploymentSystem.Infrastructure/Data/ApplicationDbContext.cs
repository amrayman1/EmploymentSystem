using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Employer> Employers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicationDetails> Applications { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employer>()
                .HasMany(e => e.Vacancies)
                .WithOne(v => v.Employer)
                .HasForeignKey(v => v.EmployerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Applications)
                .WithOne(ad => ad.Applicant)
                .HasForeignKey(ad => ad.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy>()
                .HasMany(v => v.Applications)
                .WithOne(ad => ad.Vacancy)
                .HasForeignKey(ad => ad.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationDetails>()
                .HasKey(ad => ad.Id);
        }
    }
}
