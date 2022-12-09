using System.Security.Claims;
using WebAPI.Data;

namespace WebAPI.Helper.Token
{
    public interface ITokenHelper
    {
        Task<string> GenerateAccessToken(Users user);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task<string> GenerateRefreshToken();
    }
}
