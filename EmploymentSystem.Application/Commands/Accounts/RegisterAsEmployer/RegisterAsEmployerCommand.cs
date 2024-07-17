using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Commands.Accounts.RegisterAsEmployer
{
    public class RegisterAsEmployerCommand : IRequest<IdentityResult>
    {
        //public string? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        //public string Role { get; set; } // Role selection
    }

    
}
