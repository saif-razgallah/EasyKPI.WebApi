using EasyKPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyKPI.Core.Services.Embeds
{
    public class EmbedService : IEmbedService
    {
        private Data.AppDbContext _context;

        public EmbedService(Data.AppDbContext context)
        {
            _context = context;
           
        }

        public Report GetReportById(int id)
        {
            return (DTO.Report)_context.Reports.First(n => n.Id == id );
        }
    }
}
