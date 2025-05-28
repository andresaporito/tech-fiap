using Tech.Domain.Requests;
using Tech.Domain.Responses;

namespace Tech.Domain.Interfaces.Services
{
    public interface IGameService
    {
        Task<GameResponse> AddGame(GameRequest request);
        Task<GameResponse> Delete(int id);
        Task<List<GameResponse>> GetAll();
        Task<GameResponse> GetId(int id);
        Task<GameResponse> UpdateGame(GameRequest request, int id);
    }
}
