using JwtApplication.Models;

namespace JwtApplication.Services.PasswordService
{
    public interface IPasswordInterface
    {
        void CreateHash(string password, out byte[] hash, out byte[] salt);
        public string CreateToken(UserModel user);
        public bool CheckPassword(string password, byte[] hash, byte[] salt);
    }
}
