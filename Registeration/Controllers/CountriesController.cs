using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Registeration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountry _country;

        public CountriesController(ICountry country)
        {
            _country = country;
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _country.AllCountries();
            return Ok(result);
        }

        [HttpPost("AddCountries")]
        public async Task<IActionResult> AddCountry()
        {
            var result = await _country.AddCountries();
            return Ok(result);
        }

        [HttpPost("DeleteCountries")]
        public async Task<IActionResult> DeleteCountry(string countryCode)
        {
            var result = await _country.DeleteCountry(countryCode);
            if (!result)
                return BadRequest("This country doesn't exist");
            return Ok(result);
        }
    }
}
