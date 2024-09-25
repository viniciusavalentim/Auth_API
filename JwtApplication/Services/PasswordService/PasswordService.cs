using JwtApplication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtApplication.Services.PasswordService
{
    public class PasswordService : IPasswordInterface
    {
        private readonly IConfiguration _config;    

        public PasswordService(IConfiguration config)
        {
            _config = config;
        }


        public void CreateHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key; //esse salt é uma chave que s serve para garantir que vamos conseguir criar a senha hash
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(UserModel user) 
        {
            //Claims sao basicamente o retorno que vai gerar esse token
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Cargo", user.Cargo.ToString()),
                new Claim("Email", user.Email),
                new Claim("User", user.User)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));

            //Em baixo coloco as credenciais, e qual tipo de cripitografia estou usando 
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public bool CheckPassword(string password, byte[] hash, byte[] salt)
        {
            //salt é usado para conseguir verificar que vamos conseguir validar o hash
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}
