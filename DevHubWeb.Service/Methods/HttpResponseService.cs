
using DevHubWeb.Domains;

namespace DevHubWeb.Service.Methods
{
    public class HttpResponseService
    {
        public int Ok { get { return 200; } }
        public int Accepted { get { return 202; } }
        public int NotFound { get { return 404; } }
        public int Unauthorized { get { return 401; } }

        public int Created { get { return 201; } }
        public int Updated { get { return 204; } }
        public int Conflict { get { return 409; } }
        public int BadRequest { get { return 400; } }

        public ResponseModel ShowHttpResponse(int code)
        {
            return GetCorrespondingResponse(code);
        }

        private ResponseModel GetCorrespondingResponse(int code)
        {
            switch (code)
            {
                case 201:
                    return new ResponseModel
                    {
                        StatusCode = 201,
                        Title = "CREATED",
                        Details = "The request has been fulfilled and has resulted in one or more new resources being created."
                    };

                case 202:
                    return new ResponseModel
                    {
                        StatusCode = 202,
                        Title = "ACCEPTED",
                        Details = "The request has been received but not yet acted upon."
                    };

                case 204:
                    return new ResponseModel
                    {
                        StatusCode = 204,
                        Title = "UPDATED",
                        Details = "The request has been fulfilled and has resulted in one or more new resources being modified."
                    };

                case 404:
                    return new ResponseModel
                    {
                        StatusCode = 404,
                        Title = "NOT FOUND",
                        Details = "Object cannot be found."
                    };

                case 401:
                    return new ResponseModel
                    {
                        StatusCode = 401,
                        Title = "UNAUTHORIZED",
                        Details = "The request has not been applied because it lacks valid authentication credentials for the target resource."
                    };

                case 409:
                    return new ResponseModel
                    {
                        StatusCode = 409,
                        Title = "CONFLICT",
                        Details = "The request has not been applied because it lacks valid authentication credentials for the target resource."
                    };

                case 400:
                    return new ResponseModel
                    {
                        StatusCode = 409,
                        Title = "BAD REQUEST",
                        Details = "The server cannot or will not process the request due to something that is perceived to be a client error."
                    };

                case 200:
                    return new ResponseModel
                    {
                        StatusCode = 200,
                        Title = "OK",
                        Details = "The request has succeeded." 
                    };
            }

            return new ResponseModel
            {
                StatusCode = 500,
                Title = "SERVER_ERROR",
                Details = "The server encountered an unexpected condition which prevented it from fulfilling the request."
            };
        }
    }    
}
