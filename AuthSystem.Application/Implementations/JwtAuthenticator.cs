using AuthSystem.Application.Constants;
using AuthSystem.Application.Helpers;
using AuthSystem.Application.Interfaces;
using AuthSystem.Domain.Entities;
using AuthSystem.Domain.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthSystem.Application.Implementations
{
    public class JwtAuthenticator : IJwtAuthenticator
    {
        public readonly JwtConfig _jwtConfig;

        public JwtAuthenticator(IConfiguration config, IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<JwtToken> GenerateJwtToken(User user, string[] role, string[] permissions)
        {
            string? secretKey = user.UserTypeId switch
            {
                UserType.User => _jwtConfig.UserSecret,
                UserType.Admin => _jwtConfig.AdminSecret,
                _ => throw new ArgumentException("Invalid user type", nameof(user.UserTypeId))
            };
            string userRole = user?.UserTypeId.Description();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
                {
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Role",JsonConvert.SerializeObject(role)),
                    new Claim("Permissions",JsonConvert.SerializeObject(permissions)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("FullName",$"{user.FirstName} {user.LastName} {user.MiddleName}" ),
                };

            var jwtTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtConfig.TokenTimeout);
            var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims.ToArray(),
            expires: jwtTokenExpiration,
            signingCredentials: credentials
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtToken
            {
                Email = user.Email,
                Token = jwtToken,
                Expires = jwtTokenExpiration,
                Permissions = permissions,
                UserId = user.Id.ToString(),
            };
        }
    }
}
