using Rollcall.Models;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Rollcall.Services
{
    public class JwtIssuerService
    {
        IConfiguration _configuration;
        public JwtIssuerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string createToken(User user)
        {
            var claims = new Claim[] { new Claim(ClaimTypes.Name, user.Login) };
            var credentials = getSigningCredentials();
            var options = getOptions(credentials, claims.ToList());
            return new JwtSecurityTokenHandler().WriteToken(options);
        }
        private SigningCredentials getSigningCredentials()
        {
            string? secret = _configuration.GetSection("Jwt").GetValue<string>("Secret");
            if (secret == null)
            {
                throw new InvalidDataException("In JWTIssuerService::getSigningCredentials: No jwt secret specified");
            }
            return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256);
        }
        private JwtSecurityToken getOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            var issuer = jwtConfig.GetValue<string>("Issuer");
            var audience = jwtConfig.GetValue<string>("Issuer");
            var expires = DateTime.Now.AddMinutes(jwtConfig.GetValue<double>("Expires"));

            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
        }
    }
}