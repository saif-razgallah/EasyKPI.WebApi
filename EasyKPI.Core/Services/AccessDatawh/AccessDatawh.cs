using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyKPI.Core.Services.AccessDatawh
{
    public class AccessDatawh : IAccessDatawh
    {
        private Data.AppDbContext _context;
        private readonly Data.User _user;

        public AccessDatawh(Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
               .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);


        }

        public DTO.DataWHAccess CreateDataWHAccess(Data.Models.DataWHAccess accessDataWH)
        {
            accessDataWH.user = _user;
            _context.Add(accessDataWH);
            _context.SaveChanges();

            return (DTO.DataWHAccess)accessDataWH;
        }

        public void DeleteDataWHAccess(int id)
        {
            var role = _user.Occupation;
            if (role == "Administrateur")
            {
                var accessDataWH = _context.DataWHAccess.First(n => n.Id == id);
                _context.DataWHAccess.Remove(accessDataWH);
                _context.SaveChanges();
            }
            else
            {
                var accessDataWH = _context.DataWHAccess.First(n => n.Id == id && n.user.Id == _user.Id);
                _context.DataWHAccess.Remove(accessDataWH);
                _context.SaveChanges();
            }

            
        }

        public void EditDataWHAccess(Data.Models.DataWHAccess accessDataWH)
        {
            var role = _user.Occupation;

            if (role == "Administrateur")
            {
                var editDataWhAccess = _context.DataWHAccess.First(n => n.Id == accessDataWH.Id);
                editDataWhAccess.UserAccess = accessDataWH.UserAccess;
                editDataWhAccess.DataWHId = accessDataWH.DataWHId;
                _context.SaveChanges();
            }
            else
            {
                var editDataWhAccess = _context.DataWHAccess.First(n => n.Id == accessDataWH.Id && n.user.Id == _user.Id);
                editDataWhAccess.UserAccess = accessDataWH.UserAccess;
                editDataWhAccess.DataWHId = accessDataWH.DataWHId;
                _context.SaveChanges();
            }
            
        }

        public List<DTO.DataWHAccess> GetAllDataWHAccess()
        {
            var role = _user.Occupation;

            if (role == "Administrateur")
            {
                return
                _context.DataWHAccess
                .Select(e => (DTO.DataWHAccess)e)
                .ToList();
            }
            else
            {
                return
                _context.DataWHAccess
                .Where(e => e.user.Id == _user.Id)
                .Select(e => (DTO.DataWHAccess)e)
                .ToList();
            }
                
        }

        public DTO.DataWHAccess GetDataWHAccess(int id)
        {
            return (DTO.DataWHAccess)_context.DataWHAccess.First(n => n.Id == id);
        }
    }
}
