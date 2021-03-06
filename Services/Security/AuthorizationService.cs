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
            var userCount = _repository.GetUserCount();
            return userCount == 0;
        }
        public string? AuthorizeUser(UserRegistrationDto userCred)
        {
            var user = _repository.GetUser(userCred.Login);
            if (user == null || user.PasswordSalt == null)
            {
                return null;
            }
            if (GetPasswordHash(userCred.Password, ToRawSalt(user.PasswordSalt)) == user.PasswordHash)
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
                PasswordHash = GetPasswordHash(userDto.Password, ToRawSalt(salt)),
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
            return ToCleanSalt(salt);
        }
        private string ToCleanSalt(byte[] salt) { return Convert.ToBase64String(salt); }
        private byte[] ToRawSalt(string salt) { return Convert.FromBase64String(salt); }
    }
}