using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Interfaces.Services.Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Requests;
using Tech.Domain.Responses;

namespace Tech.Services.Services
{
    public class UsersService(IUsersRepository usersRepository, IGameRepository gameRepository, IUserGameRepository userGameRepository) : IUsersService
    {
        public IUsersRepository _usersRepository { get; set; } = usersRepository;
        public IGameRepository _gameRepository { get; set; } = gameRepository;
        public IUserGameRepository _userGameRepository { get; set; } = userGameRepository;

        public async Task<UsersResponse> AddUsers(NewUsersRequest request)
        {
            var user = new Users(request.Name, request.Email, request.Password, request.Permission);

            await _usersRepository.AddUsers(user);

            return new UsersResponse
            {
                Name = user.Name
            };
        }

        public async Task<UsersResponse> Delete(int id)
        {
            var findUser = await _usersRepository.GetId(id);

            if (findUser == null)
                throw new KeyNotFoundException();

            await _usersRepository.DeleteUsers(findUser);

            return new UsersResponse
            {
                Name = findUser.Name
            };
        }

        public async Task<List<UsersResponse>> GetAll()
        {
            var users = await _usersRepository.GetAll();

            return users.Select(x => new UsersResponse
            {
                Name = x.Name
            }).ToList();
        }

        public async Task<UsersResponse> GetId(int id)
        {
            var findUser = await _usersRepository.GetId(id);

            if (findUser == null)
            {
                throw new KeyNotFoundException($"User with Id {id} not found.");
            }

            return new UsersResponse
            {
                Name = findUser.Name,
                Password = findUser.Password
            };
        }

        public async Task<UsersResponse> GetName(string name)
        {
            var findUser = await _usersRepository.GetName(name);

            if (findUser == null)
            {
                throw new KeyNotFoundException($"User with Name {name} not found.");
            }

            return new UsersResponse
            {
                Name = findUser.Name,
                Password = findUser.Password
            };
        }

        public async Task<UsersResponse> UpdateUsers(UsersRequest users, int id)
        {
            var findUser = await _usersRepository.GetId(id);

            if (findUser == null)
            {
                throw new KeyNotFoundException($"User with Id {id} not found.");
            }

            findUser.Update(users.Name, users.Password, users.Email, users.Permission);
            await _usersRepository.UpdateUsers(findUser);

            return new UsersResponse
            {
                Name = users.Name,
                Password = users.Password
            };
        }

        public async Task AddGameToUser(int userId, int gameId)
        {
            var user = await _usersRepository.GetId(userId);
            var game = await _gameRepository.GetId(gameId);

            if (user == null || game == null)
                throw new ArgumentException("User or Game not found.");

            await _userGameRepository.AddAsync(new UserGame(userId, gameId));
        }

        public async Task<IEnumerable<GameResponse>> GetUserGames(int userId)
        {
            var games = await _userGameRepository.GetGamesByUserId(userId);
            return games.Select(GameResponse.FromEntity);
        }

        public async Task RemoveGameFromUser(int userId, int gameId)
        {
            await _userGameRepository.RemoveAsync(userId, gameId);
        }
    }
}
