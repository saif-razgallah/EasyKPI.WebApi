using EasyKPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.Services.Dashboard
{
    public interface IDashboardService
    {
        Dashboards CreateDashboard(Data.Models.Dashboard dasboard);
        Dashboards GetDashboardById(int id);
        void DeleteDashboard(int id);
        List<Dashboards> GetAllDashboard(); 
    }
}
