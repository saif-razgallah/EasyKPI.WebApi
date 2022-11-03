using EasyKPI.Core.DTO;
using EasyKPI.Data;
using System.Threading.Tasks;

namespace EasyKPI.Core.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUser> SignUp(User user);
        Task<AuthenticatedUser> SignIn(User user);
    }
}
