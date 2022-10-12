using DropStorage.WebApi.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DropStorage.WebApi.Services.Services.AuthServices
{
    public class JwtTokenService
    {
        public const string ServerSigningPassword = "Knm4OpRQiM5#Q$w2#oN!ca4Z9hPC2fHY"; // TODO: user-secrets ???
        private readonly IConfiguration configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ServerSigningPassword));

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "Ampans",
                audience: "Everyone",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(this.configuration.GetAccessTokenDurationInMinutes()),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, // you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ServerSigningPassword)),
                ValidateLifetime = false, // here we are saying that we don't care about the token's expiration date
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
