using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;
using DevHubWeb.Service.Methods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service.Implementations
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITimeTrackingRepository _repo;
        private readonly HttpResponseService _response;

        public TimeTrackingService(ITimeTrackingRepository repo, HttpResponseService response)
        {
            this._repo = repo;
            this._response = response;
        }

        public Task<ActualUsageSummaryModel> DevHub_ActualUsageSummary_Get(int timeTrackerID)
        {
            return _repo.DevHub_ActualUsageSummary_Get(timeTrackerID);
        }

        public async Task<HttpResponseModel> DevHub_TimeTracking_Checkin(int bookingId, string userName)
        {
            var result = await _repo.DevHub_TimeTracking_Checkin(bookingId, userName);

            var modelForReturn = new HttpResponseModel();

            switch (result)
            {
                case -1:
                    modelForReturn.HttpStatusCode = _response.NotFound;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.NotFound);
                    modelForReturn.Remarks = "booking not found.";
                    break;

                case -2:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "already booked.";
                    break;

                case -3:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "already checked in.";
                    break;

                case 1:
                    modelForReturn.HttpStatusCode = _response.Created;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Created);
                    modelForReturn.Remarks = "successfully checked-in.";
                    break;

                case 2:
                    modelForReturn.HttpStatusCode = _response.Accepted;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Accepted);
                    modelForReturn.Remarks = "accepted";
                    break;

                case -4:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "schedule already booked.";
                    break;

                default:
                    modelForReturn.HttpStatusCode = _response.BadRequest;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
                    modelForReturn.Remarks = "Error processing this request.";
                    break;
            }

            return modelForReturn;
        }

        public async Task<HttpResponseModel> DevHub_TimeTracking_CheckOut(int timeTrackerID, string userName)
        {
            var result = await _repo.DevHub_TimeTracking_CheckOut(timeTrackerID, userName);

            var modelForReturn = new HttpResponseModel();

            switch (result)
            {
                case -1:
                    modelForReturn.HttpStatusCode = _response.NotFound;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.NotFound);
                    modelForReturn.Remarks = "not found.";
                    break;

                case -2:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "already checkout.";
                    break;

                case -3:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "already billedout.";
                    break;

                case 1:
                    modelForReturn.HttpStatusCode = _response.Created;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Created);
                    modelForReturn.Remarks = "success";
                    break;               

                default:
                    modelForReturn.HttpStatusCode = _response.BadRequest;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
                    modelForReturn.Remarks = "Error processing this request.";
                    break;
            }

            return modelForReturn;
        }

        public async Task<IEnumerable<TimeTrackingForListModel>> DevHub_TimeTracking_Get(int logStatus, DateTime dtFrom, DateTime dtTo)
        {
            return await _repo.DevHub_TimeTracking_Get(logStatus, dtFrom, dtTo);
        }
    }
}

