using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace EasyKPI.Core.Services.AccessReport
{
    public class AccessReport : IAccessReport
    {

        private Data.AppDbContext _context;
        private readonly Data.User _user;

        public AccessReport(Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
              .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);


        }


        public DTO.ReportAccess CreateReportAccess(Data.Models.ReportAccess accessReport)
        {
            accessReport.user = _user;
            _context.Add(accessReport);
            _context.SaveChanges();

            return (DTO.ReportAccess)accessReport;
        }

        public void DeleteReportAccess(int id)
        {
            var role = _user.Occupation;
            if (role == "Administrateur")
            {
                var accessReport = _context.ReportsAccess.First(n => n.Id == id);
                _context.ReportsAccess.Remove(accessReport);
                _context.SaveChanges();
            }
            else
            {
                var accessReport = _context.ReportsAccess.First(n => n.Id == id && n.user.Id == _user.Id);
                _context.ReportsAccess.Remove(accessReport);
                _context.SaveChanges();
            }
                
        }

        public void EditReportAccess(Data.Models.ReportAccess accessReport)
        {
            var role = _user.Occupation;

            if (role == "Administrateur")
            {
                var editReportAccess = _context.ReportsAccess.First(n => n.Id == accessReport.Id);
                editReportAccess.UserAccess = accessReport.UserAccess;
                editReportAccess.ReportId = accessReport.ReportId;
                _context.SaveChanges();
            }
            else
            {
                var editReportAccess = _context.ReportsAccess.First(n => n.Id == accessReport.Id && n.user.Id == _user.Id);
                editReportAccess.UserAccess = accessReport.UserAccess;
                editReportAccess.ReportId = accessReport.ReportId;
                _context.SaveChanges();
            }
                

        }

        public List<DTO.ReportAccess> GetAllReportAccess()
        {
            var role = _user.Occupation;

            if (role == "Administrateur")
            {
                return
                _context.ReportsAccess
                .Select(e => (DTO.ReportAccess)e)
                .ToList();
            }
            else
            {
                return
                 _context.ReportsAccess
                 .Where(e => e.user.Id == _user.Id)
                 .Select(e => (DTO.ReportAccess)e)
                 .ToList();
            }

           
        }

        public DTO.ReportAccess GetReportAccess(int id)
        {
            return (DTO.ReportAccess)_context.ReportsAccess.First(n => n.Id == id);
        }
    }
}
