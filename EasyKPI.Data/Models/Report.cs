using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyKPI.Data.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string Intitule { get; set; }
        public string Description { get; set; }
        public string ReportId_BI { get; set; }
        public string WorkspaceId_BI { get; set; }
        public string Status { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime DateCreation { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

    }
}
