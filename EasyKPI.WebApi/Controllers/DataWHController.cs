using EasyKPI.Core.Services.DataWarehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyKPI.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DataWHController : ControllerBase
    {

        private readonly ILogger<DataWHController> _logger;
        private IDWHService _DataWHServices;


        public DataWHController(ILogger<DataWHController> logger, IDWHService DataWHServices)
        {
            _logger = logger;
            _DataWHServices = DataWHServices;
        }

        [HttpPost]
        public IActionResult CreateDataWH(Data.Models.DataWH datawh)
        {
            var newDataWH = _DataWHServices.CreateDataWH(datawh);
            return CreatedAtRoute("GetDataWH", new { newDataWH.Id }, newDataWH);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDataWH(int id)
        {
            _DataWHServices.DeleteDataWH(id);
            return Ok();
        }

        [HttpPut]

        public IActionResult EditDataWH([FromBody] Data.Models.DataWH datawh)
        {
            _DataWHServices.EditDataWH(datawh);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetDataWH()
        {
            return Ok(_DataWHServices.GetAllDataWH());
        }

        [HttpGet("{id}", Name = "GetDataWH")]
        public IActionResult GetDataWH(int id)
        {
            return Ok(_DataWHServices.GetDataWH(id));
        }
    }
}
