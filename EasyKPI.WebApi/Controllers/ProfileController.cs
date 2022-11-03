using EasyKPI.Core.Services.Profile;
using EasyKPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IProfileService _profileService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileController(IProfileService profileService,IWebHostEnvironment hostEnvironment, AppDbContext context)
        {
            _profileService = profileService;
            this._hostEnvironment = hostEnvironment;
            _context = context;

        }

        [HttpPut("Profile")]
        public IActionResult UpdateProfile(User user)
        {
            _profileService.UpdateProfile(user);
            return Ok();
        }

        [HttpPut("Image")]
        public IActionResult UpdateImage([FromForm] User user)
        {
            user.Photo =  SaveImage(user.ImageFile);
            _profileService.UpdateImage(user);
            return Ok();
        }

        [HttpPut("Password")]
        public IActionResult UpdatePassword(User user)
        {
            _profileService.UpdatePassword(user);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserSession(int id)
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
                .Where(n => n.Id == id)
                .ToListAsync();

        }

        //[HttpGet("{username}")]
        //public async Task<ActionResult<IEnumerable<User>>> GetUserSession(string username)
        //{
        //    return await _context.Users
        //        .Select(x => new User()
        //        {
        //            Id = x.Id,
        //            Username = x.Username,
        //            Password = x.Password,
        //            Email = x.Email,
        //            FirstName = x.FirstName,
        //            LastName = x.LastName,
        //            gender = x.gender,
        //            PhoneNumber = x.PhoneNumber,
        //            Occupation = x.Occupation,
        //            Photo = x.Photo,
        //            ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.Photo)
        //        })
        //        .Where(n => n.Username == username)
        //        .ToListAsync();

        //}

        [NonAction]
        public  string SaveImage(IFormFile imageFile)
        {
            string Photo = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            Photo = Photo + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", Photo);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                 imageFile.CopyTo(fileStream);
            }

            return Photo;
        }

    }
}
