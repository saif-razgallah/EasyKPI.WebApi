using EasyKPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.Services.DataWarehouse
{
    public interface IDWHService
    {
        DataWH CreateDataWH(Data.Models.DataWH datawh);
        DataWH GetDataWH(int id);
        void DeleteDataWH(int id);
        void EditDataWH(Data.Models.DataWH datawh);
        List<DataWH> GetAllDataWH();
    }
}
