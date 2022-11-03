using System;

namespace EasyKPI.Core.DTO
{
    public class Report
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        public string Description { get; set; }
        public string ReportId_BI { get; set; }
        public string WorkspaceId_BI { get; set; }
        public string AppName_AZ { get; set; }
        public string ClientId_AZ { get; set; }
        public string ClientSecret_AZ { get; set; }
        public string TenantId_AZ { get; set; }
        public string Status { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime DateCreation { get; set; }

        public static explicit operator Report(Data.Models.Report e) => new Report
        {
            Id = e.Id,
            Intitule = e.Intitule,
            Description = e.Description,
            ReportId_BI = e.ReportId_BI,
            WorkspaceId_BI = e.WorkspaceId_BI,
            Status = e.Status,
            DateModification = e.DateModification,
            DateCreation = e.DateCreation,
        };
    }
}
