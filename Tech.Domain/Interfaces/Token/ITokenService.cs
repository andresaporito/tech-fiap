using Tech.Domain.Requests;

namespace Tech.Domain.Interfaces.Token
{
    public interface ITokenService
    {
        Task<string> GetToken(TokenRequest user);
        Task<string> ValidateInCacheToken(TokenRequest user);
    }
}
