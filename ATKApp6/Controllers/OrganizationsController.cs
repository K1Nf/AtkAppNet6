using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Domain.Enums;
using ATKApp6.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ATKApp6.Services;

namespace ATKApp6.Controllers
{
    [Route("api/ref/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OrganizationService _organizationService;

        public OrganizationsController(IConfiguration configuration, OrganizationService organizationService)
        {
            _organizationService = organizationService;
            _configuration = configuration;
        }



        [HttpGet()]
        public async Task<IActionResult> GetOrgnanizations()
        {
            return Ok(await _organizationService.GetOrganizations());
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
            Guid tokenId = Guid.Parse(User.FindFirst(_configuration.GetValue<string>("JWTConfiguration:OrgId"))!.Value);

            var departments = await _organizationService.GetDepartmentsAsync(municipality, tokenId);

            if(departments.IsFailure)
            {
                return BadRequest("Ошибка: " + departments.Error);
            }

            return Ok(departments.Value);
        }
    }
}
