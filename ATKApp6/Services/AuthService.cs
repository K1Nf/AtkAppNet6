using ATKApp6.Contracts.Request;
using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Infrastructure.Extensions;
using CSharpFunctionalExtensions;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

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


        public Result<string> Authorize(AuthorizeRequest authorizeRequest)
        {
            var organization = _dB.Organizations
                .AsNoTracking()
                .FirstOrDefault(o => o.Name == authorizeRequest.OrganizationName);
                //.Password;

            if (organization == null)
            {
                return Result.Failure<string>($"Не найден пользователь {authorizeRequest.OrganizationName}");
            }


            if (_passwordHasher.VerifyPassword(authorizeRequest.Password, organization.Password))
            {
                string token = _jwtProvider.CreateNewToken(organization.Id);
                return Result.Success(token);
            }

            return Result.Failure<string>($"Пароль неверен для {authorizeRequest.OrganizationName}!");
        }
    }
}
