
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
        
        public JwtProvider(IOptions<JWTConfiguration> options)
        {
            _jwtConfiguration = options.Value;
        }



        public string CreateNewToken(Guid userId)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey)), SecurityAlgorithms.HmacSha384);

            List<Claim> claims = new()
                {
                    new(_jwtConfiguration.OrganizationId, userId.ToString()),
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
