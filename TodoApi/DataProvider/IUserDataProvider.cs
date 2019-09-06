using TodoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User.DataProvider
{
    public interface IUserDataProvider
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(int UserId);
        Task AddUser(Users user);
        Task UpdateUser(Users users);
        Task DeleteUser(int UserId);

    }
}