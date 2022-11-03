using EasyKPI.Core;
using EasyKPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly AppDbContext _context;

        private readonly IUserService _userService;

        public UsersController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            var newUser = _userService.CreateUser(user);
            return CreatedAtRoute("GetUser", new { newUser.Id }, newUser);
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            _userService.EditUser(user);
            return Ok();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return await _context.Users
                .Select(x => new User()
                {
                    Id = x.Id,
                    Username = x.Username,
                    Password = x.Password,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    gender = x.gender,
                    PhoneNumber = x.PhoneNumber,
                    Occupation = x.Occupation,
                    Photo = x.Photo,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.Photo)
                })
                .ToListAsync();

        }

    }
}
