using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace PaymentSystem.Filters
{
    public class UserActivationChecker : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.RequestContext.HttpContext;
            if (context.User.Identity.IsAuthenticated)
            {
                var manager = context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (UserService.IsBlocked(manager, context.User.Identity.GetUserId()))
                {
                    var auth = context.GetOwinContext().Authentication;
                    auth.SignOut();
                }
                    //filterContext.Result = new RedirectResult("/Account/Logoff");
                else 
                    base.OnActionExecuting(filterContext);
            }
            else
                base.OnActionExecuting(filterContext);
        }
    }
}