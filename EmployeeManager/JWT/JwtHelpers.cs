using EmployeeManage.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EmployeeManage.JWT
{
    public static class JwtHelpers
    {
        /// <summary>
        /// To create return claims list from user token details.
        /// </summary>
        /// <param name="userAccounts"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaimDetails(this UserTokens userAccounts, DateTime expires)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.Email),
                new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Expiration, expires.ToString("MMM ddd dd yyyy HH:mm:ss tt")),
                new Claim(ClaimTypes.Expired, expires.ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }

        /// <summary>
        /// To create return claims list from user token details.
        /// </summary>
        /// <param name="userAccounts"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, DateTime expires)
        {
            return GetClaimDetails(userAccounts, expires);
        }

        /// <summary>
        /// Gen Jwt Token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="TokenType"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static string GenJwtToken(UserTokens model, string TokenType, JwtSettings jwtSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
            DateTime expireTimeJwtToken;
            if (TokenType == "AccessToken")
            {
                expireTimeJwtToken = DateTime.UtcNow.AddSeconds(jwtSettings.ExpireAccessToken);
            }
            else if (TokenType == "RefreshToken")
            {
                expireTimeJwtToken = DateTime.UtcNow.AddSeconds(jwtSettings.ExpireRefreshToken);
            }
            else
            {
                return null;
            }
            var _jwtoken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, expireTimeJwtToken),
                    expires: expireTimeJwtToken,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));
            return tokenHandler.WriteToken(_jwtoken);
        }

        /// <summary>
        /// Gen Access Token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static UserTokens GenAccessToken(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();

                if (model == null)
                    throw new ArgumentException(nameof(model));

                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                var Id = Guid.Empty;
                var expireTimeAccessToken = DateTime.UtcNow.AddSeconds(jwtSettings.ExpireAccessToken);
                var expireTimeRefreshToken = DateTime.UtcNow.AddSeconds(jwtSettings.ExpireRefreshToken);
                var _jwtoken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, expireTimeAccessToken),
                    expires: expireTimeAccessToken,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));

                userToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(_jwtoken);
                userToken.RefreshToken = GenJwtToken(model, "RefreshToken", jwtSettings);
                userToken.Validaty = expireTimeAccessToken.TimeOfDay;
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.Email = model.Email;
                userToken.ExpiredTime = expireTimeAccessToken;
                userToken.RefreshExpiredTime = expireTimeRefreshToken;

                return userToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Validate access token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public static bool ValidateToken(string token, JwtSettings jwtSettings)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    RequireExpirationTime = jwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.Zero,
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                var jwtSecurityToken = validatedToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
