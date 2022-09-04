using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication.Models;
using DapperASPNetCore.Dto;

namespace WebApplication.JwtHelpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            IEnumerable<Claim> claims = new Claim[]
                    {
                new Claim("Id",userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim("UserId", userAccounts.UserId),
                new Claim("firstname", userAccounts.firstname),
                new Claim("lastname", userAccounts.lastname),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.Role, userAccounts.role),
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim("EntityId", userAccounts.EntityId),
                new Claim(ClaimTypes.Expiration,DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt") )
               
                    };
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }
        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));

                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                var idRefreshToken = Guid.NewGuid();

                UserToken.EntityId = model.EntityId;
                UserToken.UserName = model.UserName;
                UserToken.UserId = model.UserId;
                UserToken.email = model.EmailId;
                UserToken.firstname = model.firstname;
                UserToken.lastname = model.lastname;
                UserToken.role = model.role;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // https://stackoverflow.com/questions/1458468/youtube-like-guid
        public static string IdFactory(int CharacterCount = 0)
        {
            Random random = new Random();

            // bitCount = characterCount * log (targetBase) / log(2)
            var bitCount = 6 * CharacterCount;
            var byteCount = (int)Math.Ceiling(bitCount / 8f);
            byte[] buffer = new byte[byteCount];
            random.NextBytes(buffer);

            string guid = Convert.ToBase64String(buffer);
            // Replace URL unfriendly characters
            guid = guid.Replace('+', GetRandomChar()).Replace('/', GetRandomChar());
            // Trim characters to fit the count
            return guid.Substring(0, CharacterCount);
        }

        private static char GetRandomChar()
        {
            Random rand = new Random();
            return (char)rand.Next('a', 'z');
        }


        public static IdentityDto ValidateJwtToken(string token)
        {            

            IdentityDto userClaim = new IdentityDto();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("64A63153-11C1-4919-9133-EFAF99A9B456");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
                var entityId = jwtToken.Claims.First(x => x.Type == "EntityId").Value;
                                
                userClaim.UserId = userId.ToString();
                userClaim.EntityId = entityId;

                return userClaim;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                // return null if validation fails
                return userClaim;
            }
        }

    }
}
