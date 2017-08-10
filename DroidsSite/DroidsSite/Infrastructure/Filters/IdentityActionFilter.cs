using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipstset.Authentication;
using Keys = DroidsSite.Models.Keys;

namespace DroidsSite.Infrastructure.Filters
{
    public class IdentityActionFilter : ActionFilterAttribute
    {
        private string _connection = MvcApplication.DroidsConnection;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var service = new UserService(new UserServiceRepository(_connection), Keys.CookieKeys.IdentityTokenCookie, Keys.CookieKeys.TraceTokenCookie, new TimeSpan(182, 0, 0, 0), new TimeSpan(365, 0, 0, 0));
            //check for cookie
            var idCookie = HttpContext.Current.Request.Cookies[Keys.CookieKeys.IdentityTokenCookie];
            var token = idCookie != null ? idCookie.Value : string.Empty;
            service.ProcessIdentity(token);
        }
    }
}