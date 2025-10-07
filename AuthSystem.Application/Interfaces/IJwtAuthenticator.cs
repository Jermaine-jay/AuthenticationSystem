using AuthSystem.Application.Helpers;
using AuthSystem.Domain.Entities;

namespace AuthSystem.Application.Interfaces
{
    public interface IJwtAuthenticator
    {
        Task<JwtToken> GenerateJwtToken(User user, string[] role, string[] permissions);
    }
}
