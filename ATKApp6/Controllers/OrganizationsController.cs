using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Domain.Enums;
using ATKApp6.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ATKApp6.Controllers
{
    [Route("api/ref/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly DataBaseContext _dB;
        private readonly JWTConfiguration _jwtConfiguration;

        public OrganizationsController(DataBaseContext dB, IOptions<JWTConfiguration> options)
        {
            _dB = dB;
            _jwtConfiguration = options.Value;
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
        public async Task<IActionResult> GetDepartaments([FromQuery] string? municipality)
        {
            if (!string.IsNullOrEmpty(municipality))
            {
                Municipalities? municipalityEnum = EnumHelper.GetEnumValueFromEnumMember<Municipalities>(municipality);

                var departments = await _dB.Organizations
                    .Where(x => x.Municipality == municipalityEnum)
                    .Select(m => m.Name)
                    .ToListAsync();

                return Ok(departments);
            }

            Guid tokenId = Guid.Parse(User.FindFirst(_jwtConfiguration.OrganizationId)!.Value);

            var municipalityOrganization = await _dB.Organizations
                .AsNoTracking()
                .Where(x => x.Id == tokenId)
                .FirstOrDefaultAsync();

            if (municipalityOrganization == null)
            {
                return NotFound("Организация не найдена.");
            }

            var deps = await _dB.Organizations
                    .Where(x => x.Municipality == municipalityOrganization.Municipality)
                    .Select(m => m.Name)
                    .ToListAsync();

            return Ok(deps);
        }
    }
}
