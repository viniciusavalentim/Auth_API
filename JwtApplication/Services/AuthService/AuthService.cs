using JwtApplication.Database;
using JwtApplication.Dtos;
using JwtApplication.Models;
using JwtApplication.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace JwtApplication.Services.UserServices
{
    public class AuthService : IAuthInterface
    {
        //injeção de dependencia
        private readonly AppDbContext _context;
        private readonly IPasswordInterface _passwordService;


        public AuthService(AppDbContext context, IPasswordInterface passwordService)
        {
            this._context = context;
            this._passwordService = passwordService;
        }

        public async Task<Response<string>> Register(CreateUsersDto registerUser)
        {
            Response<string> response = new Response<string>();

            try
            {
                if (ExistUser(registerUser))
                {
                    response.Data = null;
                    response.Message = "Email ou usuario ja cadastrados";
                    response.Status = false;
                    return response;
                }

                _passwordService.CreateHash(registerUser.Password, out byte[] hash, out byte[] salt);

                UserModel user = new UserModel()
                {
                    User = registerUser.User,
                    Email = registerUser.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                var token = _passwordService.CreateToken(user);

                response.Message = "Usuario cadastrado com sucesso";
                response.Data = token;

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<string>> Login(LoginUsersDto loginUser)
        {
            Response<string> response = new Response<string>();

            try
            {
                var emailUser = await _context.Users.FirstOrDefaultAsync(userDatabase => userDatabase.Email == loginUser.Email);

                if (emailUser == null)
                {
                    response.Data = null;
                    response.Message = "Email não cadastrado";
                    response.Status = false;
                    return response;
                }



                if (!_passwordService.CheckPassword(loginUser.Password, emailUser.PasswordHash, emailUser.PasswordSalt))
                {
                    response.Data = null;
                    response.Message = "Senha incorreta, Tente novamente";
                    response.Status = false;
                }
                else
                {
                    var token = _passwordService.CreateToken(emailUser);

                    response.Data = token;
                    response.Message = "User Logado com sucesso!";
                    response.Status = true;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ex.Message;
                response.Status = false;
            }

            return response;

        }

        public bool ExistUser(CreateUsersDto user)
        {
            var userVerif = _context.Users.FirstOrDefault(userDatabase => userDatabase.Email == user.Email || userDatabase.User == user.User);
            return userVerif != null ? true : false;
        }
    }
}
