using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Domains
{
    public class HttpResponseModel
    {
        public int HttpStatusCode { get; set; }
        public ResponseModel ResponseModel { get; set; }
        public string Remarks { get; set; }
    }

    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
