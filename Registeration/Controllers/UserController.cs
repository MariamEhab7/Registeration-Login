using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Registeration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Dependancy Injection
        private readonly IUser _users;
        private readonly ICountry _country;

        public UserController(IUser users, ICountry country)
        {
            _users = users;
            _country = country;
        }
        #endregion

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var user = await _users.GetUserAsync(id);
            if (user == null)
                return NotFound("invalid id");
            await _users.DeleteUserAsync(user);
            return NoContent();
        }
    }
}
