using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyKPI.Data.Models
{
    public class Dashboard
    {
        [Key]
        public int Id { get; set; }
        public int ReportId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

    }
}
