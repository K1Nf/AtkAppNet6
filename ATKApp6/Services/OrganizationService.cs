using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;
using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Infrastructure.Extensions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ATKApp6.Services
{
    public class OrganizationService
    {
        private readonly DataBaseContext _dB;
        private readonly ILogger<OrganizationService> _logger;

        public OrganizationService(DataBaseContext dB, ILogger<OrganizationService> logger)
        {
            _dB = dB;
            _logger = logger;
        }


        public async Task<List<Organization>> GetOrganizations() => await _dB.Organizations.ToListAsync();


        public async Task<Result<List<StructuredOrganizations>>> GetDepartmentsAsync(string? municipality, Guid tokenId)
        {
            if (!string.IsNullOrEmpty(municipality))
            {
                Municipalities? municipalityEnum = EnumHelper.GetEnumValueFromEnumMember<Municipalities>(municipality);

                var departments = await _dB.Organizations
                    .Where(x => x.Municipality == municipalityEnum)
                    .Select(m => m.Name)
                .ToListAsync();

                return Result.Success(departments);
            }


            var municipalityOrganization = await _dB.Organizations
                .AsNoTracking()
                .Where(x => x.Id == tokenId)
                .FirstOrDefaultAsync();

            if (municipalityOrganization == null)
            {
                return Result.Failure<List<StructuredOrganizations>>("Ваша Организация не найдена");
            }

            var deps = await _dB.Organizations
                    .Where(x => x.Municipality == municipalityOrganization.Municipality)
                    .Select(m => m.Name)
                    .ToListAsync();

            return Result.Success(deps);
        }
    }
}
