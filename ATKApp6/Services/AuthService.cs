using ATKApp6.Contracts.Request;
using ATKApp6.Domain.Enums;
using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Infrastructure.Extensions;
using CSharpFunctionalExtensions;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ATKApp6.Services
{
    public class AuthService
    {
        private readonly DataBaseContext _dB;
        private readonly IPasswordHash _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public AuthService(DataBaseContext dB,
                            IPasswordHash passwordHasher,
                            IJwtProvider jwtProvider)
        {
            _dB = dB;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }


        public Result<(string, string)> Authorize(AuthorizeRequest authorizeRequest)
        {
            var organization = _dB.Organizations
                .AsNoTracking()
                .FirstOrDefault(o => o.Name == authorizeRequest.OrganizationName);

            if (organization == null)
            {
                return Result.Failure<(string, string)>($"Не найдена организация \"{authorizeRequest.OrganizationName}\"");
            }


            char[] pwdChars = authorizeRequest.Password.ToCharArray();
            string? russianOrganizationName = EnumHelper.GetEnumMemberValueByName<StructuredOrganizations>(authorizeRequest.OrganizationName.ToString());

            try
            {
                if (_passwordHasher.VerifyPassword(pwdChars, organization.Password))
                {
                    string token = _jwtProvider.CreateNewToken(organization.Id);
                    return Result.Success((token, russianOrganizationName ?? authorizeRequest.OrganizationName.ToString()));
                }
            }
            finally
            {
                Array.Clear(pwdChars, 0, pwdChars.Length);
            }

            return Result.Failure<(string, string)>($"Пароль неверен для \"{russianOrganizationName ?? "ВАШЕЙ ОРГАНИЗАЦИИ"}\"!");
        }
    }
}
