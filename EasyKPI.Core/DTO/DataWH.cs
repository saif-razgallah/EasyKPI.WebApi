using System;

namespace EasyKPI.Core.DTO
{
    public class DataWH
    {
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

        public static explicit operator DataWH(Data.Models.DataWH e) => new DataWH
        {
            Id = e.Id,
            Intitule = e.Intitule,
            ConnectionType = e.ConnectionType,
            DataWHName = e.DataWHName,
            DataWHServer = e.DataWHServer,
            DataWHUser = e.DataWHUser,
            DataWHPassword = e.DataWHPassword,
            AuthWindows = e.AuthWindows,
            Status = e.Status,
            DateModification = e.DateModification,
            DateCreation = e.DateCreation
        };
    }
}
