using EasyKPI.Core.Services.Embeds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EasyKPI.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmbeddingController : ControllerBase
    {
        private readonly ILogger<EmbeddingController> _logger;
        private IEmbedService _EmbedService;

        public EmbeddingController(ILogger<EmbeddingController> logger, IEmbedService EmbedService)
        {
            _logger = logger;
            _EmbedService = EmbedService;
        }

        [HttpGet("{id}")]
        public async Task<EmbedInfo> GetEmbeddingById(int id)
        {
            var report = _EmbedService.GetReportById(id);

            Guid workspaceId = Guid.Parse(report.WorkspaceId_BI);
            Guid reportId = Guid.Parse(report.ReportId_BI);

            var embedInfo = await Embedder.GetEmbedInfo(workspaceId, reportId);
            return embedInfo;
        }
    }
}
