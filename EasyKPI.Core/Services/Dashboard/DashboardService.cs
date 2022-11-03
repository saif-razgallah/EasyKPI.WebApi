using EasyKPI.Core.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyKPI.Core.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {

        private Data.AppDbContext _context;
        private readonly Data.User _user;

        public DashboardService(Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
               .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);


        }

        public Dashboards CreateDashboard(Data.Models.Dashboard dasboard)
        {
            dasboard.user = _user;
            _context.Add(dasboard);
            _context.SaveChanges();

            return (DTO.Dashboards)dasboard;
        }

        public void DeleteDashboard(int id)
        {
            var dasboard = _context.Dashboard.First(n => n.Id == id && n.user.Id == _user.Id);
            _context.Dashboard.Remove(dasboard);
            _context.SaveChanges();
        }

        public List<Dashboards> GetAllDashboard()
        {
            return
                 _context.Dashboard
                 .Where(e => e.user.Id == _user.Id)
                 .Select(e => (DTO.Dashboards)e)
                 .ToList();
        }

        public Dashboards GetDashboardById(int id)
        {
            return (DTO.Dashboards)_context.Dashboard.First(n => n.Id == id);
        }
    }
}
