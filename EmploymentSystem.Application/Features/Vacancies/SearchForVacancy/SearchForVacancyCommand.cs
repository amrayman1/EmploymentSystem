using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.SearchForVacancy
{
    public class SearchVacanciesQuery : IRequest<IEnumerable<Vacancy>>
    {
        public string Title { get; set; }
    }

    
}
