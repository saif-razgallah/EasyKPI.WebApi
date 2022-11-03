using EasyKPI.Core.Services.Dashboard;
using EasyKPI.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EasyKPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private IDashboardService _DashboardService;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService DashboardService)
        {
            _logger = logger;
            _DashboardService = DashboardService;
        }

        [HttpPost]
        public IActionResult CreateDashboard(Dashboard dasboard)
        {
            var newDasboard = _DashboardService.CreateDashboard(dasboard);
            return CreatedAtRoute("GetDasboard", new { newDasboard.Id }, newDasboard);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDasboard(int id)
        {
            _DashboardService.DeleteDashboard(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllDashboard()
        {
            return Ok(_DashboardService.GetAllDashboard());
        }

        [HttpGet("{id}", Name = "GetDasboard")]
        public IActionResult GetDashboard(int id)
        {
            return Ok(_DashboardService.GetDashboardById(id));
        }
    }
}
