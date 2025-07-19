using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Domain.Enums;
using ATKApp6.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ATKApp6.Controllers
{
    [Route("api/ref/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly DataBaseContext _dB;
     
        public OrganizationsController(DataBaseContext dB)
        {
            _dB = dB;
        }



        [HttpGet()]
        public async Task<IActionResult> GetOrgnanizations()
        {
            return Ok(await _dB.Organizations.ToListAsync());
        }



        [HttpGet("municipalities")]
        [Authorize]
        public async Task<IActionResult> GetMunicipalities()
        {
            Municipalities[] municipalities = (Municipalities[])Enum.GetValues(typeof(Municipalities));
            return Ok(municipalities);
        }



        [HttpGet("departments")]
        [Authorize]
        public async Task<IActionResult> GetDepartaments([FromQuery] string municipality)
        {
            Municipalities? municipalityEnum = EnumHelper.GetEnumValueFromEnumMember<Municipalities>(municipality);

            var departments = await _dB.Organizations
                .Where(x => x.Municipality == municipalityEnum)
                .Select(m => m.Name)
                .ToListAsync();

            return Ok(departments);
        }
    }
}
