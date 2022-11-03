using EasyKPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.Services.AccessDatawh
{
    public interface IAccessDatawh
    {
        DataWHAccess CreateDataWHAccess(Data.Models.DataWHAccess accessDataWH);
        DataWHAccess GetDataWHAccess(int id);
        void DeleteDataWHAccess(int id);
        void EditDataWHAccess(Data.Models.DataWHAccess accessDataWH);
        List<DataWHAccess> GetAllDataWHAccess();
    }
}
