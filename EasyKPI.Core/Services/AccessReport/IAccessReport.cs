using EasyKPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.Services.AccessReport
{
    public interface IAccessReport
    {
        ReportAccess CreateReportAccess(Data.Models.ReportAccess accessReport);
        ReportAccess GetReportAccess(int id);
        void DeleteReportAccess(int id);
        void EditReportAccess(Data.Models.ReportAccess accessReport);
        List<ReportAccess> GetAllReportAccess();

    }
}
