namespace Tech.Domain.Interfaces.Services
{
    using global::Tech.Domain.Entities;

    namespace Tech.Domain.Interfaces.Repositories
    {
        public interface IUserGameRepository
        {
            Task AddAsync(UserGame userGame);
            Task<IEnumerable<Game>> GetGamesByUserId(int userId);
            Task RemoveAsync(int userId, int gameId);
        }
    }

}
