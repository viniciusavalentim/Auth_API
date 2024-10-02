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
            var response = await _authInterface.Login(userLogin);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }


        [HttpPost("register")]
        //o metodo assincrono faz com que ele espera a chamada do banco de realizar para assim continuar o código;
        //Task<ActionResult> = retuorna uma tarefa do tipo actionResult
        public async Task<ActionResult> Register(CreateUsersDto user)
        {
            var response = await _authInterface.Register(user);
            if (response.Status == false) { 
                return BadRequest(response.Data);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
