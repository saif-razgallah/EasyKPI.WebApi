using EasyKPI.Core.CustomExceptions;
using EasyKPI.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EasyKPI.Core.Services.Profile
{
    public class ProfileService : IProfileService
    {

        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public ProfileService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }

        public User GetProfileDetails(string username)
        {
            return _context.Users.First(n => n.Username == username);
        }

        public void UpdateImage(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");
            user.Photo = userParam.Photo;

            _context.SaveChanges();

        }

        public void UpdatePassword(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            if (_passwordHasher.VerifyHashedPassword(user.Password, userParam.oldpassword) ==
              PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid password");
            }

            if (_passwordHasher.VerifyHashedPassword(user.Password, userParam.oldpassword) !=
               PasswordVerificationResult.Failed)
            {
                user.Password = _passwordHasher.HashPassword(userParam.Password);
            }

            _context.SaveChanges();
        }

        public void UpdateProfile(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new Exception("Username is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            user.Email = userParam.Email;
            user.gender = userParam.gender;
            user.PhoneNumber = userParam.PhoneNumber;

            _context.SaveChanges();
        }
    }
}
