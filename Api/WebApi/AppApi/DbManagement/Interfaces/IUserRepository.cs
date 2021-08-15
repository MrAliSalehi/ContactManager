using AppApi.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppApi.DbManagement.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user,int id);
        Task<List<User>> Search(string data);
        Task<User> RemoveUser(int id);
        Task<bool> IsExists(User user);
        Task<User> SearchByID(int id);
    }
}
