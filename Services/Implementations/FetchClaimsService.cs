using EmployeeLoginPortal.Models.ResponseViewModels;
using EmployeeLoginPortal.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeLoginPortal.Services.Implementations
{
    public class FetchClaimsService : IFetchClaimsService
    {

        public ClaimsViewModel FetchClaims(string jwtToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = tokenHandler.ReadJwtToken(jwtToken);

            ClaimsViewModel claims = new ClaimsViewModel();

            foreach (Claim claim in jwt.Claims)
            {
                switch (claim.Type)
                {
                    case JwtRegisteredClaimNames.Sub:
                        if (int.TryParse(claim.Value, out int userId))
                        {
                            claims.UserId = userId;
                        }
                        break;
                    case JwtRegisteredClaimNames.Iat:
                        claims.IssuedAt = claim.Value;
                        break;
                    case JwtRegisteredClaimNames.Typ:
                        claims.Role = claim.Value;
                        break;
                    case JwtRegisteredClaimNames.Exp:
                        if (long.TryParse(claim.Value, out long UnixTime))
                        {
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(UnixTime);
                            claims.ExpiredAt = dateTimeOffset.DateTime;
                        }
                        break;
                }
            }
            return claims;
        }

        public ClaimsViewModel IsTokenValid(string jwtToken, string publicKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(publicKey));
            

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
            JwtSecurityToken jwt = tokenHandler.ReadJwtToken(jwtToken);
            ClaimsViewModel claims = new ClaimsViewModel();
            foreach (var claim in jwt.Claims)
            {
                switch (claim.Type)
                {
                    case JwtRegisteredClaimNames.Sub:
                        if (int.TryParse(claim.Value, out int userId))
                        {
                            claims.UserId = userId;
                        }
                        break;
                    case JwtRegisteredClaimNames.Iat:
                        
                            claims.IssuedAt = claim.Value;

                        break;

                    case JwtRegisteredClaimNames.Typ:
                        claims.Role = claim.Value;
                        break;
                    case JwtRegisteredClaimNames.Exp:
                        if (long.TryParse(claim.Value, out long expiryUnixTime))
                        {
                            DateTimeOffset expiryTime = DateTimeOffset.FromUnixTimeSeconds(expiryUnixTime);
                            claims.ExpiredAt = expiryTime.DateTime;
                            claims.IsValid = DateTime.UtcNow < expiryTime;
                        }
                        break;
                }
            }
            return claims;
        }
    }
}
