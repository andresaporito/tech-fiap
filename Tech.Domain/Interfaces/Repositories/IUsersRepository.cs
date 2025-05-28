using Tech.Domain.Entities;

namespace Tech.Domain.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> GetId(int id);
        Task<List<Users>> GetAll();
        Task<Users> AddUsers(Users Users);
        Task<Users> UpdateUsers(Users Users);
        Task<Users> DeleteUsers(Users Users);
        Task<Users> GetName(string name);
    }
}
