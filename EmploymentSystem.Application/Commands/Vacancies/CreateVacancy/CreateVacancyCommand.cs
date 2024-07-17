using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Commands.Vacancies.CreateVacancy
{
    public class CreateVacancyCommand : IRequest<Vacancy>
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int MaxApplications { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

}
