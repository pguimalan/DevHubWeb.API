using System;
using System.Threading.Tasks;
using DevHubWeb.Service;
using DevHubWeb.Service.Methods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevHubWeb.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [Authorize(Roles = "Admin, User, Super User")]
    public class TimeTrackingController : BaseController
    {
        private readonly ITimeTrackingService _service;
        private readonly HttpResponseService _response;        

        public TimeTrackingController(ITimeTrackingService service, HttpResponseService response)
        {
            this._service = service;
            this._response = response;
        }
        [HttpPost("CheckIn/{id}")]
        public async Task<IActionResult> TimeTracking_Checkin(int id)
        {
            var result = await _service.DevHub_TimeTracking_Checkin(id, _baseUserName);           
            if(result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });

            return BadRequest();
        }

        [HttpPost("CheckOut/{id}")]
        public async Task<IActionResult> TimeTracking_CheckOut(int id)
        {
            var result = await _service.DevHub_TimeTracking_CheckOut(id, _baseUserName);
            if (result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> DevHub_TimeTracking_Get()
        {
            var result = await _service.DevHub_TimeTracking_Get(98,DateTime.Now, DateTime.Now);
            return Ok(result);
        }

        [HttpGet("Get/{dtFrom}/{dtTo}")]
        public async Task<IActionResult> DevHub_TimeTracking_Get(DateTime dtFrom, DateTime dtTo)
        {
            var result = await _service.DevHub_TimeTracking_Get(99, dtFrom, dtTo);
            return Ok(result);
        }

        [HttpGet("GetActualUsage/{id}")]
        public async Task<IActionResult> GetActualUsage(int id)
        {
            var result = await _service.DevHub_ActualUsageSummary_Get(id);
            return Ok(result);
        }
    }
}