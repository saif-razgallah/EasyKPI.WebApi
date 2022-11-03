using EasyKPI.Core.CustomExceptions;
using EasyKPI.Core.Services;
using EasyKPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyKPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                var result = await _authenticationService.SignUp(user);
                return Created("", result);
            }
            catch (UsernameAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(User user)
        {

            try
            {
                var result = await _authenticationService.SignIn(user);
                return Ok(result);

            }
            catch (InvalidUsernamePasswordException e)
            {
                return StatusCode(409, e.Message);
            }


        }
    }
}







