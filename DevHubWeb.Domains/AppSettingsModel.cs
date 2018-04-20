using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Domains
{
    public class AppSettingsModel
    {
        //SwaggerAuth
        public string ApiKey { get; set; }

        //Email
        public string SenderEmail { get; set; }
        public string Password { get; set; }

        //WebConfig
        public string Protocol { get; set; }

        //ConnectionString
        public string DevHubDBConn { get; set; }

        //JWT
        public string JwtKey { get; set; }
    }
}
