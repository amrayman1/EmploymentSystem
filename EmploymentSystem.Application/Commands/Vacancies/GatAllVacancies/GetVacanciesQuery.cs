using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.GatAllVacancies
{
    public class GetVacanciesQuery : IRequest<IEnumerable<Vacancy>>
    {
    }

    
}
