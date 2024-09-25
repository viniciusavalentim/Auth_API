using JwtApplication.Dtos;
using JwtApplication.Models;

namespace JwtApplication.Services.UserServices
{
    public interface IAuthInterface
    {
        //Interface é como se fosse um contrato.
        Task<Response<string>> Register(CreateUsersDto registerUser);
        Task<Response<string>> Login(LoginUsersDto loginUser);
    }
}
