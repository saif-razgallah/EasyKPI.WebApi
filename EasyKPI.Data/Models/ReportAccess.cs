using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyKPI.Data.Models
{
    public class ReportAccess
    {
        [Key]
        public int Id { get; set; }
        public int UserAccess { get; set; }
        public int ReportId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

        [ForeignKey("ReportId")]
        public Report report { get; set; }

    }
}
