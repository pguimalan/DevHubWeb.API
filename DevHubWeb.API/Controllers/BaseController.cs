using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevHubWeb.API.Controllers
{
    public class BaseController : Controller
    {
        private const string _baseUrlhelper = "UrlHelper";
        protected string _baseUserName;
        protected string _baseUri;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _baseUserName = HttpContext.User.Identity.Name;            
            context.HttpContext.Items[_baseUrlhelper] = Url;
            _baseUri = HttpContext.Request.Host.Value;
        }
    }
}