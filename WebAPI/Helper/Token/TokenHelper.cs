using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Data;
using WebAPI.Helper.SearchRole;

namespace WebAPI.Helper.Token
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly IUsertoRole _usertoRole;

        public TokenHelper(IConfiguration configuration, UserManager<Users> userManager, IUsertoRole usertoRole)
        {
            _configuration = configuration;
            _userManager = userManager;
            _usertoRole = usertoRole;   
     

        }

        public async Task<string> GenerateAccessToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

           var roles =await _usertoRole.RoleName(user);
           
            var claims = new List<Claim> {

            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email,user.Email),
			 new Claim("UserAvatar",$"{user.UserAvatar}")
            //new Claim(ClaimTypes.Role,role)
            };

            

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
                

            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail),
                SecurityAlgorithms.HmacSha256Signature)

            };
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            return  tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidAudience = _configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyDetail)
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameter, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Geçersiz Token!");
            return principal;
        }

       
    }
}
