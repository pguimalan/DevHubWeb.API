using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevHubWeb.Domains;
using DevHubWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHubWeb.API.Controllers
{
    [Produces("application/json")]
    [Route("/bookLog")]
    public class BookLogController : BaseController
    {
        private readonly IBookLogService _service;

        public BookLogController(IBookLogService service)
        {
            this._service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> BookLogCreate([FromBody] BookLogForCreateModel model)
        {            
            // validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var uri = HttpContext.Request.Host.Value;
            if (model.BookingTypeID == 3)
            {
                model.UserName = "SYSTEM";
            }
            else model.UserName = _baseUserName;

            var result = await _service.BookLog_Set(model, _baseUri);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("Get/{dtFrom}/{dtTo}"), Authorize(Roles = "Admin, User, Super User")]
        public async Task<IActionResult> BookLogList_Get(string dtFrom, string dtTo)
        {
            var bookLogList = await _service.BookLogList_Get(dtFrom, dtTo);            
            
            return Ok(bookLogList);
        }

        [HttpGet("GetBookLogBlockingSchedule/{id}")]        
        public async Task<IActionResult> GetBookLogBlockingSchedule_Get(int id)
        {
            var schedList = await _service.BookLogBlockingSchedule_Get(id);
            
            return Ok(schedList);
        }
    }
}