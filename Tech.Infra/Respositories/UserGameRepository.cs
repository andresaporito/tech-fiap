namespace Tech.Infra.Respositories
{
    using global::Tech.Domain.Entities;
    using global::Tech.Domain.Interfaces.Services.Tech.Domain.Interfaces.Repositories;
    using global::Tech.Infra.Context;
    using Microsoft.EntityFrameworkCore;

    namespace Tech.Infra.Repositories
    {
        public class UserGameRepository : IUserGameRepository
        {
            private readonly AppDbContext _context;

            public UserGameRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(UserGame userGame)
            {
                _context.UserGames.Add(userGame);
                await _context.SaveChangesAsync();
            }

            public async Task<IEnumerable<Game>> GetGamesByUserId(int userId)
            {
                return await _context.UserGames
                    .Where(ug => ug.UserId == userId)
                    .Include(ug => ug.Game)
                    .Select(ug => ug.Game)
                    .ToListAsync();
            }

            public async Task RemoveAsync(int userId, int gameId)
            {
                var entity = await _context.UserGames
                    .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GameId == gameId);

                if (entity != null)
                {
                    _context.UserGames.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}
