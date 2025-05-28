using Tech.Domain.Requests;
using Tech.Domain.Responses;

namespace Tech.Domain.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UsersResponse> AddUsers(NewUsersRequest request);
        Task<List<UsersResponse>> GetAll();
        Task<UsersResponse> GetId(int id);
        Task<UsersResponse> GetName(string name);
        Task<UsersResponse> UpdateUsers(UsersRequest request, int id);
        Task<UsersResponse> Delete(int id);
    }
}
