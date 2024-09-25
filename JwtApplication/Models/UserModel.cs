using JwtApplication.Enum;

namespace JwtApplication.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? User { get; set; }
        public CargoEnum Cargo { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime TokenCreate { get; set; } = DateTime.Now;
    }
        
}
