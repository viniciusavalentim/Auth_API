﻿using JwtApplication.Enum;
using System.ComponentModel.DataAnnotations;

namespace JwtApplication.Dtos
{
    public class CreateUsersDto
    {
        [Required(ErrorMessage = "O campo User é obgratorio")]
        public string? User { get; set; }

        [Required(ErrorMessage = "O campo Email é obgratorio"), EmailAddress(ErrorMessage = "Inválid Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obgratorio")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Senhas nao coincidem")]
        public string? ConfirmPassword { get; set; }


    }
}
