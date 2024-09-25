using JwtApplication.Dtos;
using JwtApplication.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;

        public AuthController(IAuthInterface auth)
        {
            this._authInterface = auth;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUsersDto userLogin)
        {
            return Ok(await _authInterface.Login(userLogin));
        }


        [HttpPost("register")]
        //o metodo assincrono faz com que ele espera a chamada do banco de realizar para assim continuar o código;
        //Task<ActionResult> = retuorna uma tarefa do tipo actionResult
        public async Task<ActionResult> Register(CreateUsersDto user)
        {
            return Ok(await _authInterface.Register(user));
        }
    }
}
