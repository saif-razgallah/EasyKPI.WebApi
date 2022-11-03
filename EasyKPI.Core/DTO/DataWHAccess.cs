namespace EasyKPI.Core.DTO
{
    public class DataWHAccess
    {
        public int Id { get; set; }
        public int UserAccess { get; set; }
        public int DataWHId { get; set; }




        public static explicit operator DataWHAccess(Data.Models.DataWHAccess e) => new DataWHAccess
        {
            Id = e.Id,
            UserAccess = e.UserAccess,
            DataWHId = e.DataWHId,

        };
    }
}
