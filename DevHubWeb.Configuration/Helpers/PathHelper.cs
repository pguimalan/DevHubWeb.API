using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Configuration.Helpers
{
    public static class PathHelper
    {
        public static string GetParentFolder(IHostingEnvironment env)
        {
            var parentfolder = env.ContentRootPath.Replace(env.ApplicationName, "DevHubWeb.Data");
            return parentfolder;
        }

    }
}
