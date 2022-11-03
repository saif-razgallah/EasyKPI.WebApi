using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyKPI.Data.Models
{
    public class DataWHAccess
    {
        [Key]
        public int Id { get; set; }
        public int UserAccess { get; set; }

        public int DataWHId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

        [ForeignKey("DataWHId")]
        public DataWH DataWH { get; set; }

    }
}
