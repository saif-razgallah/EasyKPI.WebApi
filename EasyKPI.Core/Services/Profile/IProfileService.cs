

using EasyKPI.Data;

namespace EasyKPI.Core.Services.Profile
{
    public interface IProfileService
    {

        void UpdateImage(User user);
        void UpdateProfile(User user);
        void UpdatePassword(User user);
        User GetProfileDetails(string username);
    }
}
