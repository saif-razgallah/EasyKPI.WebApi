using EasyKPI.Core.Services.AccessReport;
using EasyKPI.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EasyKPI.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccessReportController : ControllerBase
    {

        private readonly ILogger<AccessReportController> _logger;
        private IAccessReport _AccessReport;

        public AccessReportController(ILogger<AccessReportController> logger, IAccessReport AccessReport)
        {
            _logger = logger;
            _AccessReport = AccessReport;
        }


        [HttpPost]
        public IActionResult CreateAccessReport(ReportAccess accessReport)
        {
            var newAccessReport = _AccessReport.CreateReportAccess(accessReport);
            return CreatedAtRoute("GetReport", new { newAccessReport.Id }, newAccessReport);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccessReport(int id)
        {
            _AccessReport.DeleteReportAccess(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllAccessReport()
        {
            return Ok(_AccessReport.GetAllReportAccess());
        }

        [HttpGet("{id}", Name = "GetAccessReport")]
        public IActionResult GetAccessReport(int id)
        {
            return Ok(_AccessReport.GetReportAccess(id));
        }

        [HttpPut]
        public IActionResult EditAccessReport([FromBody] ReportAccess accessReport)
        {
            _AccessReport.EditReportAccess(accessReport);
            return Ok();
        }

    }
}
