using AzureOcrFlutterAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureOcrFlutterAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
