using ATKApp6.Domain.Enums;
using ATKApp6.Domain.Models;
using ATKApp6.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static CSharpFunctionalExtensions.Result;

namespace ATKApp6.Infrastructure.Extensions.Authorization
{
    public class AuthorizeHandler : AuthorizationHandler<PolicyNameRequirements>
    {
        private readonly DataBaseContext _dB;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public AuthorizeHandler(IConfiguration configuration, DataBaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dB = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyNameRequirements requirement)
        {
            Claim? claim = context.User.Claims
                .SingleOrDefault(c => c.Type == _configuration.GetValue<string>("JWTConfiguration:OrgId"));


            if (claim == null)
            {
                context.Fail();
                return;
            }

            Guid tokenId = Guid.Parse(claim!.Value);
            
            string? fullPath = _httpContextAccessor.HttpContext!.Request.Path.Value;


            if (fullPath == null)
            {
                context.Fail();
                return;
            }
            

            Guid eventId = Guid.Parse(fullPath.Split("/")[^1]);


            //Guid? organizationOwnerId = _dB.EventsBase
            //    .AsNoTracking()
            //    .Include(e => e.Organizer)
            //    .FirstOrDefault(e => e.Id == eventId)?
            //    .OrganizerId;


            Organization? organizationOwner = _dB.EventsBase
                .AsNoTracking()
                .Include(e => e.Organizer)
                .FirstOrDefault(e => e.Id == eventId)?
                .Organizer;


            //Organization? authorOrganization = await _dB.Organizations
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(o => o.Id == organizationOwnerId);


            Organization? tokenOrganization = await _dB.Organizations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == tokenId);



            if (tokenOrganization == null || organizationOwner == null) // РАЗДЕЛИТЬ, ТАК КАК ОДНА ОШИБКА ГОВОРИТ, ЧТО НЕ НАЙДЕНО ТАКОЕ МЕРОПРИЯТИЕ С АЙДИ,
            {                                                           // А ВТОРАЯ, ЧТО НЕ ПРАВИЛЬНО АУТЕНТИФИЦИРОВАН ПОЛЬЗОВАТЕЛЬ
                context.Fail();                                         // ОБРАБОТКА ВОЗВРАТА РЕЗУЛЬТАТА С СООБЩЕНИЕМ, В ЗАВИСИМОСТИ ОТ ТОГО,
                return;                                                 // ПОЧЕМУ НЕ ПРОЙДЕНА ПРОВЕРКА
            }

            if (tokenId == organizationOwner.Id)           // ПОЛЬЗОВАТЕЛЬ ОТКРЫЛ СВОЕ МЕРОПРИЯТИЕ
            {
                context.Succeed(requirement);
                return;
            }

            if (tokenOrganization.Municipality == Municipalities.noMunicipality) // СОТРУДНИК АТК ХМАО ОТКРЫЛ МЕРОПРИЯТИЕ (НЕ ВАЖНО ЧЬЕ)
            {
                context.Succeed(requirement);
                return;
            }

            string name = tokenOrganization.Name.ToString();

            if (tokenOrganization.Name.ToString().StartsWith("atk_") 
                && tokenOrganization.Municipality == organizationOwner.Municipality) // СОТРУДНИК МУНИЦИПАЛЬНОГО АТК
            {                                                                        // ОТКРЫВАЕТ МЕРОПРИЯТИЕ СВОЕ ИЛИ СВОЕЙ ОРГАНИЗАЦИИ
                context.Succeed(requirement);
                return;
            }


            context.Fail();
            return;
        }
    }
}
