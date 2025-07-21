
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATKApp6.Infrastructure.Extensions
{
    public class JwtProvider : IJwtProvider 
    {
        private readonly JWTConfiguration _jwtConfiguration;
        private readonly IConfiguration _configuration;

        public JwtProvider(IOptions<JWTConfiguration> options, IConfiguration configuration)
        {
            _jwtConfiguration = options.Value;
            _configuration = configuration;
        }


        public string CreateNewToken(Guid userId)
        {
            var secretKey = _configuration.GetValue<string>("JWTConfiguration:SecretKey");
            var claimValue = _configuration.GetValue<string>("JWTConfiguration:OrgId");

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha384);

            List<Claim> claims = new()
                {
                    new(claimValue, userId.ToString()),
                };


            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiresMinutes),
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
