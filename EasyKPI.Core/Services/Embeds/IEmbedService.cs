using EasyKPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.Services.Embeds
{
    public interface IEmbedService
    {
        Report GetReportById(int id);
    }
}
