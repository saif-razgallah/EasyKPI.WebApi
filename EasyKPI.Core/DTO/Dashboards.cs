using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.DTO
{
    public class Dashboards
    {
        public int Id { get; set; }
        public int ReportId { get; set; }

        public static explicit operator Dashboards(Data.Models.Dashboard e) => new Dashboards
        {
            Id = e.Id,
            ReportId = e.ReportId,

        };
    }
}
