using System.Security.Claims;

namespace AbySalto.Mid.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
    }
}
