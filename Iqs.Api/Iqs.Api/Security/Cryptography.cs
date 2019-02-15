using Iqs.Api.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Iqs.Api.Security
{
    public static class Cryptography
    {
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static string GenerateTokenByClaims(IEnumerable<Claim> claims) {
            if (claims == null || claims.Count() == 0) {
                throw new Exception("Cant find claims (is null or empty)");
            }

            var dateNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: dateNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static MethodResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token) {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = false,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
            };

            SecurityToken securityToken;

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                return "Invalid token".ToErrorMethodResult<ClaimsPrincipal>();
            }

            return principal.ToSuccessMethodResult();
        }
    }
}
