﻿using System.ComponentModel.DataAnnotations;

namespace EmploymentSystem.Presentation.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
