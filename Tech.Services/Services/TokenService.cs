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
    public class TokenService(IConfiguration configuration, IUsersRepository usersRepository, IMemoryCache memoryCache) : ITokenService

    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly IMemoryCache _memoryCache = memoryCache;

        public async Task<string> GetToken(TokenRequest user)
        {
            var findUser = await _usersRepository.GetName(user.Name);

            if (findUser == null)
                return string.Empty;

            if (!(findUser.Name == user.Name && findUser.Password == user.Password))
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();

            var chave = Encoding.ASCII.GetBytes(_configuration.GetSection("SecretJwt").Value);

            var tokenProp = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, findUser.Name),
                    new Claim(ClaimTypes.Role, findUser.Permission.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenProp);

            return tokenHandler.WriteToken(token);


        }

        public async Task<string> ValidateInCacheToken(TokenRequest user)
        {
            var token = string.Empty;

            try
            {
                MemoryCacheEntryOptions op = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8)
                };

                _memoryCache.TryGetValue($"Token_{user.Name}", out token);

                if (string.IsNullOrEmpty(token))
                {
                    token = await GetToken(user);
                    _memoryCache.Set($"Token_{user.Name}", token, op);
                }

                return token;


            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
