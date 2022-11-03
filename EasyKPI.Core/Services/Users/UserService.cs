using EasyKPI.Core.CustomExceptions;
using EasyKPI.Core.DTO;
using EasyKPI.Core.Utilities;
using EasyKPI.Data;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyKPI.Core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public object Request { get; private set; }

        public UserService(AppDbContext context,IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }

        public User CreateUser(User user)
        {
            var checkUser = _context.Users
               .FirstOrDefault(u => u.Username.Equals(user.Username));

            if (checkUser != null)
            {
                throw new Exception("Username Already Exists");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void EditUser(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new Exception("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            user.Email = userParam.Email;
            user.gender = userParam.gender;
            user.PhoneNumber = userParam.PhoneNumber;
            user.Occupation = userParam.Occupation;
            user.Photo = userParam.Photo;

            if ( _passwordHasher.VerifyHashedPassword(user.Password, userParam.Password) ==
               PasswordVerificationResult.Failed)
            {
                user.Password = _passwordHasher.HashPassword(userParam.Password);
            }
            

            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;

        }

        public User GetUserById(int id)
        {
            return _context.Users.First(n => n.Id == id);

        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
           
        }

        
    }
}
