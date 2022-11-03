namespace EasyKPI.Core.DTO
{
    public class ReportAccess
    {
        public int Id { get; set; }
        public int UserAccess { get; set; }
        public int ReportId { get; set; }




        public static explicit operator ReportAccess(Data.Models.ReportAccess e) => new ReportAccess
        {
            Id = e.Id,
            UserAccess = e.UserAccess,
            ReportId = e.ReportId,

        };

    }
}
