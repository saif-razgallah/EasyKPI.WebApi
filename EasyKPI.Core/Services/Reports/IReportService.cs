using EasyKPI.Core.DTO;
using System.Collections.Generic;

namespace EasyKPI.Core.Services.Reports
{
    public interface IReportService
    {
        Report CreateReport(Data.Models.Report report);
        Report GetReport(int id);
        void DeleteReport(int id);
        void EditReport(Data.Models.Report report);
        List<Report> GetAllReport();
    }
}
