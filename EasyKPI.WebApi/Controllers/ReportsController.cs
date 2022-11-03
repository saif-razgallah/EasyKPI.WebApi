using EasyKPI.Core.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EasyKPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {

        private readonly ILogger<ReportsController> _logger;
        private IReportService _ReportServices;

        public ReportsController(ILogger<ReportsController> logger, IReportService ReportServices)
        {
            _logger = logger;
            _ReportServices = ReportServices;
        }

        [HttpPost]
        public IActionResult CreateReport(Data.Models.Report report)
        {
            var newReport = _ReportServices.CreateReport(report);
            return CreatedAtRoute("GetReport", new { newReport.Id }, newReport);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
            _ReportServices.DeleteReport(id);
            return Ok();
        }

        [HttpPut]

        public IActionResult EditReport([FromBody] Data.Models.Report report)
        {
            _ReportServices.EditReport(report);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetReport()
        {
            return Ok(_ReportServices.GetAllReport());
        }

        [HttpGet("{id}", Name = "GetReport")]
        public IActionResult GetReport(int id)
        {
            return Ok(_ReportServices.GetReport(id));
        }


    }
}
