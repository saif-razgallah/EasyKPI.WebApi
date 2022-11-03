using EasyKPI.Core.Services.AccessDatawh;
using EasyKPI.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EasyKPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccessDatawhController : ControllerBase
    {
        private readonly ILogger<AccessDatawhController> _logger;
        private IAccessDatawh _AccessDatawh;

        public AccessDatawhController(ILogger<AccessDatawhController> logger, IAccessDatawh AccessDatawh)
        {
            _logger = logger;
            _AccessDatawh = AccessDatawh;
        }


        [HttpPost]
        public IActionResult CreateAccessDataWH(DataWHAccess AccessDatawh)
        {
            var newAccessDatawh = _AccessDatawh.CreateDataWHAccess(AccessDatawh);
            return CreatedAtRoute("GetAccessDatawh", new { newAccessDatawh.Id }, newAccessDatawh);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccessDatawh(int id)
        {
            _AccessDatawh.DeleteDataWHAccess(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllAccessDatawh()
        {
            return Ok(_AccessDatawh.GetAllDataWHAccess());
        }

        [HttpGet("{id}", Name = "GetAccessDatawh")]
        public IActionResult GetAccessReport(int id)
        {
            return Ok(_AccessDatawh.GetDataWHAccess(id));
        }

        [HttpPut]
        public IActionResult EditAccessDatawh([FromBody] DataWHAccess AccessDatawh)
        {
            _AccessDatawh.EditDataWHAccess(AccessDatawh);
            return Ok();
        }
    }
}
