using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Commands.Vacancies.ApplyForVacancy
{
    public class ApplyForVacancyCommand : IRequest<ApplicationDetails>
    {
        public int VacancyId { get; set; }
        //public string ApplicantId { get; set; }
    }
}
