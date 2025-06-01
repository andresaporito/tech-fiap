using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Requests;
using Tech.Domain.Responses;

namespace Tech.Services.Services
{
    public class GameService(IGameRepository gameRepository) : IGameService
    {
        public IGameRepository _gameRepository { get; set; } = gameRepository;

        public async Task<GameResponse> AddGame(GameRequest request)
        {
            var game = new Game(request.Nome, request.Categoria, request.Plataforma, request.DataLancamento);
            await _gameRepository.AddGame(game);
            return GameResponse.FromEntity(game);
        }

        public async Task<GameResponse> Delete(int id)
        {
            var game = await _gameRepository.GetId(id);

            if (game == null)
                throw new KeyNotFoundException($"Game with Id {id} not found.");

            await _gameRepository.DeleteGame(game);
            return GameResponse.FromEntity(game);
        }

        public async Task<List<GameResponse>> GetAll()
        {
            var games = await _gameRepository.GetAll();
            return games.Select(GameResponse.FromEntity).ToList();
        }

        public async Task<GameResponse> GetId(int id)
        {
            var game = await _gameRepository.GetId(id);
            return game == null ? null : GameResponse.FromEntity(game);
        }

        public async Task<GameResponse> UpdateGame(GameRequest request, int id)
        {
            var game = await _gameRepository.GetId(id);

            if (game == null)
                throw new KeyNotFoundException($"Game with Id {id} not found.");

            game.Update(request.Nome, request.Categoria, request.Plataforma, request.DataLancamento);
            await _gameRepository.UpdateGame(game);
            return GameResponse.FromEntity(game);
        }
    }
}
