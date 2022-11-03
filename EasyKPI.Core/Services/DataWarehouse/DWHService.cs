using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyKPI.Core.Services.DataWarehouse
{
    public class DWHService : IDWHService
    {

        private Data.AppDbContext _context;

        private readonly Data.User _user;

        public DWHService(Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);

        }
        public DTO.DataWH CreateDataWH(Data.Models.DataWH datawh)
        {
            datawh.user = _user;
            _context.Add(datawh);
            _context.SaveChanges();

            return (DTO.DataWH)datawh;
        }

        public void DeleteDataWH(int id)
        {
            var role = _user.Occupation;
            if (role == "Administrateur")
            {
                var datawh = _context.DataWH.First(n => n.Id == id );
                _context.DataWH.Remove(datawh);
                _context.SaveChanges();
            }
            else
            {
                var datawh = _context.DataWH.First(n => n.Id == id && n.user.Id == _user.Id);
                _context.DataWH.Remove(datawh);
                _context.SaveChanges();
            }
               
        }

        public void EditDataWH(Data.Models.DataWH datawh)
        {
            var role = _user.Occupation;
            if (role == "Administrateur")
            {
                var editDatawh = _context.DataWH.First(n => n.Id == datawh.Id);
                editDatawh.Intitule = datawh.Intitule;
                editDatawh.ConnectionType = datawh.ConnectionType;
                editDatawh.DataWHName = datawh.DataWHName;
                editDatawh.DataWHServer = datawh.DataWHServer;
                editDatawh.DataWHUser = datawh.DataWHUser;
                editDatawh.DataWHPassword = datawh.DataWHPassword;
                editDatawh.AuthWindows = datawh.AuthWindows;
                editDatawh.Status = datawh.Status;
                //editDatawh.DateModification = datawh.DateModification;
                _context.SaveChanges();
            }
            else
            {
                var editDatawh = _context.DataWH.First(n => n.Id == datawh.Id && n.user.Id == _user.Id);
                editDatawh.Intitule = datawh.Intitule;
                editDatawh.ConnectionType = datawh.ConnectionType;
                editDatawh.DataWHName = datawh.DataWHName;
                editDatawh.DataWHServer = datawh.DataWHServer;
                editDatawh.DataWHUser = datawh.DataWHUser;
                editDatawh.DataWHPassword = datawh.DataWHPassword;
                editDatawh.AuthWindows = datawh.AuthWindows;
                editDatawh.Status = datawh.Status;
                //editDatawh.DateModification = datawh.DateModification;
                _context.SaveChanges();
            }
        }

        public List<DTO.DataWH> GetAllDataWH()
        {
            
            var role = _user.Occupation;

            if (role == "Administrateur")
            {
                return
                _context.DataWH
                .Select(e => (DTO.DataWH)e)
                .ToList();
            }
            else if (role == "Gestionnaire")
            {
                return
                _context.DataWH
                .Where(e => e.user.Id == _user.Id)
                .Select(e => (DTO.DataWH)e)
                .ToList();
            }
            else
            {
                return
                _context.DataWHAccess
                .Where(e => e.UserAccess == _user.Id)
                .Select(e => (DTO.DataWH)e.DataWH)
                .ToList();
            }
        }

        public DTO.DataWH GetDataWH(int id)
        {
            return (DTO.DataWH)_context.DataWH.First(n => n.Id == id && n.user.Id == _user.Id);
        }
    }
}
