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

            return new GameResponse
            {
                Nome = game.Nome,
                Categoria = game.Categoria,
                Plataforma = game.Plataforma,
                DataLancamento = game.DataLancamento
            };
        }

        public async Task<GameResponse> Delete(int id)
        {
            var findGame = await _gameRepository.GetId(id);

            if (findGame == null)
                throw new KeyNotFoundException($"Jogo com Id {id} não foi encontrado.");

            await _gameRepository.DeleteGame(findGame);

            return new GameResponse
            {
                Nome = findGame.Nome,
                Categoria = findGame.Categoria,
                Plataforma = findGame.Plataforma,
                DataLancamento = findGame.DataLancamento
            };
        }

        public async Task<List<GameResponse>> GetAll()
        {
            var games = await _gameRepository.GetAll();

            return games.Select(x => new GameResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Categoria = x.Categoria,
                Plataforma = x.Plataforma,
                DataLancamento = x.DataLancamento
            }).ToList();
        }

        public async Task<GameResponse> GetId(int id)
        {
            var game = await _gameRepository.GetId(id);

            if (game == null) return null;

            return new GameResponse
            {
                Id = game.Id,
                Nome = game.Nome,
                Categoria = game.Categoria,
                Plataforma = game.Plataforma,
                DataLancamento = game.DataLancamento
            };
        }

        public async Task<GameResponse> UpdateGame(GameRequest request, int id)
        {
            var game = await _gameRepository.GetId(id);

            if (game == null)
                throw new KeyNotFoundException($"Jogo com o Id {id} não foi encontrado.");

            game.Alterar(request.Nome, request.Categoria, request.Plataforma, request.DataLancamento);
            await _gameRepository.UpdateGame(game);

            return new GameResponse
            {
                Id = game.Id,
                Nome = game.Nome,
                Categoria = game.Categoria,
                Plataforma = game.Plataforma,
                DataLancamento = game.DataLancamento
            };
        }
    }
}
