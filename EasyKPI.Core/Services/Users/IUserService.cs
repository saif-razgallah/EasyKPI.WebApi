using EasyKPI.Core.DTO;
using EasyKPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyKPI.Core
{
    public interface IUserService
    {

        User CreateUser(User user);
        void EditUser(User user);
        void DeleteUser(int id);
        User GetUserById(int id);
        List<User> GetAllUsers();



    }
}
