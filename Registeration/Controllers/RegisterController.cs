using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Registeration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        #region Dependancy Injection
        private readonly IRegister _regester;

        public RegisterController(IRegister regester)
        {
            _regester = regester;
        }
        #endregion


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> UserRegesterAsync([FromForm] AddUserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _regester.UserRegesterAsync(model);

            if (!user)
            {
                return BadRequest("This email already exists!");
            }
            if(user == null)
                return BadRequest("invalid Email");

            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _regester.GetUserByEmailAsync(model.Email);
            if (user == null)
                return NotFound("invalid username or password");

            var result = await _regester.LoginAsync(model);
            if(!result)
                return BadRequest("invalid username or password");

            return Ok(user);
        } 
        
        [HttpPost]
        [Route("VerifyCode")]
        public async Task<IActionResult> Verification(int verifyCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _regester.Verification(verifyCode);
            if (!result)
                return BadRequest("Wrong Code!");

            return Ok(result);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync(Guid id)
        {
            await _regester.LogoutAsync(id);
            return NoContent();
        }
    }
}
