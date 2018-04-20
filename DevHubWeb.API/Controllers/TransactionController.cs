using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHubWeb.Domains;
using DevHubWeb.Service;
using DevHubWeb.Service.Methods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHubWeb.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [Authorize(Roles = "Admin, User, Super User")]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _service;
        private readonly HttpResponseService _response;

        public TransactionController(ITransactionService service, HttpResponseService response)
        {
            this._service = service;
            this._response = response;
        }

        [HttpPost("TransAddItem")]
        public async Task<IActionResult> TransAddItem([FromBody] TransOthersForCreateUpdate model)
        {
            var result = await _service.Billing_TransOthers_Set(model);
            if (result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });

            return BadRequest();
        }

        [HttpGet("TransGetItem/{timeTrackerId}")]
        public async Task<IActionResult> TransGetItem([FromRoute] int timeTrackerId)
        {
            return Ok(await _service.Billing_TransOthers_List(timeTrackerId));
        }

        [HttpPost("TransRemoveItem/{id}")]
        public async Task<IActionResult> TransRemoveItem([FromRoute] int id)
        {
            var result = await _service.Billing_TransOthers_Remove(id);
            if (result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });
            return BadRequest();
        }

        [HttpPost("TransAddMiscellaneous")]
        public async Task<IActionResult> TransAddMiscellaneous([FromBody] TransMiscellaneousForCreateUpdate model)
        {
            var result = await _service.Billing_TransMiscellaneous_Set(model);
            if (result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });

            return BadRequest();
        }

        [HttpGet("TransGetMiscellaneous/{timeTrackerId}")]
        public async Task<IActionResult> TransGetMiscellaneous([FromRoute] int timeTrackerId)
        {
            return Ok(await _service.Billing_TransMiscellaneous_List(timeTrackerId));
        }

        [HttpPost("TransRemoveMiscellaneous/{id}")]
        public async Task<IActionResult> TransRemoveMiscellaneous([FromRoute] int id)
        {
            var result = await _service.Billing_TransMiscellaneous_Remove(id);
            if (result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });
            return BadRequest();
        }

        [HttpGet("TransGetBillingSummary/{timeTrackerId}")]
        public async Task<IActionResult> BillingSummary_Get([FromRoute] int timeTrackerId)
        {
            return Ok(await _service.DevHub_BillingSummary_Get(timeTrackerId));
        }

        [HttpPost("TransBilling")]
        public async Task<IActionResult> TransBilling([FromBody] TransBillingModel model)
        {
            model.UserName = _baseUserName;
            var result = await _service.Billing_OverallTransaction_Set(model);
            if (result != null)
                return StatusCode(result.HttpStatusCode, new { response = result.ResponseModel, remaks = result.Remarks });
            return BadRequest();
        }
    }
}