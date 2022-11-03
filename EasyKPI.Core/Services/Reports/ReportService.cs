using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace EasyKPI.Core.Services.Reports
{
    public class ReportService : IReportService
    {

        private Data.AppDbContext _context;

        private readonly Data.User _user;

        public ReportService(Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);

        }

        public DTO.Report CreateReport(Data.Models.Report report)
        {
            report.user = _user;
            _context.Add(report);
            _context.SaveChanges();

            return (DTO.Report)report;
        }

        public void DeleteReport(int id)
        {
            var role = _user.Occupation;
            if (role == "Administrateur")
            {
                var report = _context.Reports.First(n => n.Id == id );
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }
            else
            {
                var report = _context.Reports.First(n => n.Id == id && n.user.Id == _user.Id);
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }

                
        }

        public void EditReport(Data.Models.Report report)
        {
            var role = _user.Occupation;

            if (role == "Administrateur")
            { 
                var editReport = _context.Reports.First(n => n.Id == report.Id );
                editReport.Intitule = report.Intitule;
                editReport.Description = report.Description;
                editReport.ReportId_BI = report.ReportId_BI;
                editReport.WorkspaceId_BI = report.WorkspaceId_BI;
                editReport.Status = report.Status;
                _context.SaveChanges();
            }
            else 
            { 
                var editReport = _context.Reports.First(n => n.Id == report.Id && n.user.Id == _user.Id);
                editReport.Intitule = report.Intitule;
                editReport.Description = report.Description;
                editReport.ReportId_BI = report.ReportId_BI;
                editReport.WorkspaceId_BI = report.WorkspaceId_BI;
                editReport.Status = report.Status;
                //editReport.DateModification = report.DateModification;
                //editReport.DateCreation = report.DateCreation;
                _context.SaveChanges();
            }
            
           
        }
        public List<DTO.Report> GetAllReport()
        {
            var role = _user.Occupation;

            if (role == "Administrateur")
            {
                return
               _context.Reports
               .Select(e => (DTO.Report)e)
               .ToList();
            }
            else if (role == "Concepteur") 
            { 
                return
                     _context.Reports
                    .Where(e => e.user.Id == _user.Id)
                    .Select(e => (DTO.Report)e)
                    .ToList();
            } 
            else
            {
                return 
                   _context.ReportsAccess
                   .Where(e => e.UserAccess == _user.Id)
                   .Select(e => (DTO.Report)e.report)
                   .ToList();
            }
                    
        }

        public DTO.Report GetReport(int id)
        {
            return (DTO.Report)_context.Reports.First(n => n.Id == id && n.user.Id == _user.Id);
        }
    }
}
