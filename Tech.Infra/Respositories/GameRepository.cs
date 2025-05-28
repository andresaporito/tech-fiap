using Microsoft.EntityFrameworkCore;
using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Infra.Context;

namespace Tech.Infra.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Game> GetId(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Game>> GetAll()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> AddGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<Game> UpdateGame(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<Game> DeleteGame(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }
    }
}
