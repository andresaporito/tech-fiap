using Tech.Domain.Entities;

namespace Tech.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<Game> GetId(int id);
        Task<List<Game>> GetAll();
        Task<Game> AddGame(Game game);
        Task<Game> UpdateGame(Game game);
        Task<Game> DeleteGame(Game game);
    }
}
