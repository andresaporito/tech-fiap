using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Token;
using Tech.Domain.Requests;

namespace Tech.Services.Services
{
    public class TokenService(
        IConfiguration configuration,
        IUsersRepository usersRepository,
        IMemoryCache memoryCache
    ) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly IMemoryCache _memoryCache = memoryCache;

        public async Task<string> GetToken(TokenRequest user)
        {
            var findUser = await _usersRepository.GetName(user.Name);

            if (findUser == null || findUser.Password != user.Password)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["SecretJwt"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, findUser.Name),
                new Claim(ClaimTypes.Role, findUser.Permission.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> ValidateInCacheToken(TokenRequest user)
        {
            var cacheKey = $"Token_{user.Name}";

            if (_memoryCache.TryGetValue(cacheKey, out string token) && !string.IsNullOrEmpty(token))
                return token;

            token = await GetToken(user);

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Invalid credentials");

            _memoryCache.Set(cacheKey, token, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8)
            });

            return token;
        }
    }
}
