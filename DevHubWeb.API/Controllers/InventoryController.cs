using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevHubWeb.Configuration.Helpers;
using DevHubWeb.Domains;
using DevHubWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHubWeb.API.Controllers
{
    [Produces("application/json")]
    [Route("/Inventory")]
    [Authorize(Roles = "Admin, User, Super User")]
    public class InventoryController : BaseController
    {
        private readonly IInventoryService _service;
        private readonly HttpResponseHelper _response;

        public InventoryController(IInventoryService service, HttpResponseHelper response)
        {
            this._service = service;
            this._response = response;
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> AddUpdate([FromBody] InventoryModel model)
        {
            model.Username = _baseUserName;
            var result = await _service.CreateUpdate(model, _baseUserName);

            if (result.Action == 1)
                return StatusCode((int)HttpStatusCode.Created, new { data = result, states = _response.Created });

            else if(result.Action == 2)
                return StatusCode((int)HttpStatusCode.OK, new { data = result, states = _response.Updated });

            else
                return StatusCode((int)HttpStatusCode.BadRequest, new { data = result, states = _response.BadRequest });
            
        }

        [HttpGet("History")]
        public async Task<IActionResult> GetHistory(int id, DateTime dateFrom, DateTime dateTo, bool isSetDefault, int product)
        {
            if (product > 0)
            {
                return Ok(new
                {
                    data = await _service.GetByProductId(product)
                });
            }
            if (id > 0)
            {
                return Ok(new
                {
                    data = await _service.GetById(id)
                });
            }
            else
            {
                return Ok(new
                {
                    data = await _service.GetInventoryHistory(dateFrom, dateTo, isSetDefault)
                });
            }
        }

        [HttpGet("Products")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(new
            {
                data = await _service.GetInventory(id)
            });
        }
    }
}