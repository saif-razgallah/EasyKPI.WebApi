using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyKPI.Data.Models
{
    public class DataWH
    {
        [Key]
        public int Id { get; set; }
        public string Intitule { get; set; }
        public string ConnectionType { get; set; }
        public string DataWHName { get; set; }
        public string DataWHServer { get; set; }
        public string DataWHUser { get; set; }
        public string DataWHPassword { get; set; }
        public string AuthWindows { get; set; }
        public string Status { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime DateCreation { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

    }
}
