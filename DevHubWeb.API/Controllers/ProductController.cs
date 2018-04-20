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
    [Route("/Product")]
    [Authorize(Roles = "Admin, User, Super User")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly HttpResponseHelper _response;
        private readonly IInventoryService _invService;

        public ProductController(IProductService service, HttpResponseHelper response, IInventoryService invService)
        {
            this._service = service;
            this._response = response;
            this._invService = invService;
        }

        [HttpPost("CreateUpdate")]
        public async Task<IActionResult> CreateUpdate([FromBody] ProductModel model)
        {
            var result = await _service.CreateUpdate(model);

            if(result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks ,});

            return BadRequest();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var list = await _service.Get("");
            return Ok(new
            {
                data = list
            });
        }

        [HttpGet("GetByName/{prodName}")]
        public async Task<IActionResult> GetByName(string prodName)
        {
            var list = await _service.Get(prodName);
            if (list == null || list.Count() <= 0)
                return StatusCode((int)HttpStatusCode.NotFound, new { data = "No Record Found." });

            return Ok(new
            {
                data = list
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var list = await _service.GetById(id);
            if (list == null)
                return StatusCode((int)HttpStatusCode.NotFound, new { data = "No Record Found." });
            return Ok(new
            {
                data = list
            });

        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(new
            {
                data = await _invService.GetCategories()
            });
        }
        
        [HttpGet("UnitOfMeasure")]
        public async Task<IActionResult> GetUnitOfMeasure()
        {
            return Ok(new
            {
                data = await _invService.GetUnitOfMeasure()
            });
        }
    }
}