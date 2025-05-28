using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Requests;
using Tech.Domain.Responses;

namespace Tech.Services.Services
{
    public class UsersService(IUsersRepository UsersRepository) : IUsersService

    {
        public IUsersRepository _usersRepository { get; set; } = UsersRepository;

        public async Task<UsersResponse> AddUsers(NewUsersRequest request)
        {
            var Users = new Users(request.Name, request.Password, request.Email, request.Permission);

            await _usersRepository.AddUsers(Users);

            return new UsersResponse
            {
                Name = Users.Name
            };
        }

        public async Task<UsersResponse> Delete(int id)
        {
            var findUsers = await _usersRepository.GetId(id);

            if (findUsers == null)
                throw new KeyNotFoundException();

            await _usersRepository.DeleteUsers(findUsers);

            return new UsersResponse
            {
                Name = findUsers.Name
            };
        }

        public async Task<List<UsersResponse>> GetAll()
        {
            var findUsers = await _usersRepository.GetAll();

            var response = findUsers.Select(x => new UsersResponse
            {
                Name = x.Name
            }).ToList();

            return response;
        }

        public async Task<UsersResponse> GetId(int id)
        {
            var findUsers = await _usersRepository.GetId(id);

            if (findUsers == null)
            {
                throw new KeyNotFoundException($"Usuario com o Id {id} não foi encontrado.");
            }

            return new UsersResponse
            {
                Name = findUsers.Name,
                Password = findUsers.Password
            };
        }

        public async Task<UsersResponse> GetName(string name)
        {
            var findUsers = await _usersRepository.GetName(name);

            if (findUsers == null)
            {
                throw new KeyNotFoundException($"Usuario com o Name {name} não foi encontrado.");
            }

            return new UsersResponse
            {
                Name = findUsers.Name,
                Password = findUsers.Password
            };
        }

        public async Task<UsersResponse> UpdateUsers(UsersRequest users, int id)
        {
            var findUsers = await _usersRepository.GetId(id);

            if (findUsers == null)
            {
                throw new KeyNotFoundException($"Usuario com o Id {id} não foi encontrado.");
            }

            findUsers.Update(users.Name, users.Password, users.Email, users.Permission);
            await _usersRepository.UpdateUsers(findUsers);

            return new UsersResponse
            {
                Name = users.Name,
                Password = users.Password
            };
        }
    }
}
