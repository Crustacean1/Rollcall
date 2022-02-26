using Rollcall.Models;
using Rollcall.Repositories;

using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Rollcall.Services
{
    public class AuthorizationService
    {
        private readonly UserRepository _repository;
        private readonly JwtIssuerService _issuerService;
        private int _keyIterationCount = 100000;
        private int _keyBitLength = 256 / 8;
        private int _saltBitLength = 8;
        public AuthorizationService(UserRepository repository, JwtIssuerService jwtIssuer)
        {
            _repository = repository;
            _issuerService = jwtIssuer;
        }
        public bool ShouldRegisterUser(UserRegistrationDto userCred)
        {
            var _user = _repository.GetUserCount();
            return _user == 0;
        }
        public string? AuthorizeUser(UserRegistrationDto userCred)
        {
            var user = _repository.GetUser(userCred.Login);
            if (user == null || user.PasswordSalt == null)
            {
                return null;
            }
            if (GetPasswordHash(userCred.Password, toRawSalt(user.PasswordSalt)) == user.PasswordHash)
            {
                return _issuerService.createToken(user);
            }
            return null;
        }
        public async Task CreateUser(UserRegistrationDto userDto)
        {
            var salt = GenerateRandomSalt();
            User user = new User
            {
                Login = userDto.Login,
                PasswordHash = GetPasswordHash(userDto.Password, toRawSalt(salt)),
                PasswordSalt = salt
            };
            _repository.AddUser(user);
            await _repository.SaveChangesAsync();
        }
        private string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, _keyIterationCount, _keyBitLength));
        }
        private string GenerateRandomSalt()
        {
            var salt = new byte[_saltBitLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(salt);
            }
            return toCleanSalt(salt);
        }
        private string toCleanSalt(byte[] salt) { return Convert.ToBase64String(salt); }
        private byte[] toRawSalt(string salt) { return Convert.FromBase64String(salt); }
    }
}