using System.ComponentModel.DataAnnotations;

namespace JwtApplication.Dtos
{
    public class LoginUsersDto
    {
        [Required(ErrorMessage = "Email nao informado"), EmailAddress(ErrorMessage = "Invalid email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is obg")]
        public string? Password{ get; set; }
    }
}
