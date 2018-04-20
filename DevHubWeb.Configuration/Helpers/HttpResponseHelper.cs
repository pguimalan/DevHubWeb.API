using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Configuration.Helpers
{
    public class HttpResponseHelper
    {        
        public int Ok { get { return 200; } }
        public int NotFound { get { return 404; } }
        public int Unauthorized { get { return 401; } }

        public int Created { get { return 201; } }
        public int Updated { get { return 204; } }
        public int Conflict { get { return 409; } }
        public int BadRequest { get { return 400; } }        

        public Response ShowHttpResponse(int code)
        {
            return GetCorrespondingResponse(code);
        }

        private Response GetCorrespondingResponse(int code)
        {
            switch (code)
            {
                case 201:
                    return new Response
                    {
                        StatusCode = 200,
                        Title = "CREATED",
                        Details = "The request has been fulfilled and has resulted in one or more new resources being created."
                    };

                case 204:
                    return new Response
                    {
                        StatusCode = 204,
                        Title = "UPDATED",
                        Details = "The request has been fulfilled and has resulted in one or more new resources being modified."
                    };

                case 404:
                    return new Response
                    {
                        StatusCode = 404,
                        Title = "NOT FOUND",
                        Details = "Object cannot be found."
                    };

                case 401:
                    return new Response
                    {
                        StatusCode = 401,
                        Title = "UNAUTHORIZED",
                        Details = "The request has not been applied because it lacks valid authentication credentials for the target resource."
                    };

                case 409:
                    return new Response
                    {
                        StatusCode = 409,
                        Title = "CONFLICT",
                        Details = "The request has not been applied because it lacks valid authentication credentials for the target resource."
                    };

                case 400:
                    return new Response
                    {
                        StatusCode = 409,
                        Title = "BAD REQUEST",
                        Details = "The server cannot or will not process the request due to something that is perceived to be a client error."
                    };

                default:
                    return new Response
                    {
                        StatusCode = 200,
                        Title = "OK",
                        Details = "The request could not be completed due to a conflict with the current state of the target resource. This code is used in situations where the user might be able to resolve the conflict and resubmit the request."
                    };
            }
        }
    }

    public class Response
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
