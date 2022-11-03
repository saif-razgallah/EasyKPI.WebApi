using EasyKPI.Core.CustomExceptions;
using EasyKPI.Core.DTO;
using EasyKPI.Core.Utilities;
using EasyKPI.Data;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EasyKPI.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public AuthenticationService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            //password
            if (dbuser == null
                || _passwordHasher.VerifyHashedPassword(dbuser.Password, user.Password) ==
                PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            var id = dbuser.Id;
            return new AuthenticatedUser
            {
                UserId = id,
                Username = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));
            

            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username Already Exists");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            var dbuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            var id = dbuser.Id;
            return new AuthenticatedUser
            {
                UserId = id,
                Username = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };

        }
    }
}
