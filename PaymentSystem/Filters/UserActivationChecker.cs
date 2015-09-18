using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using DAO.Repository;
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
            var factory = (IRepositoryFactory)DependencyResolver.Current.GetService(typeof(IRepositoryFactory));
            if (context.User.Identity.IsAuthenticated)
            {
                var manager = context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = factory.GetUserRepository(manager).FindById(context.User.Identity.GetUserId());
                if (user != null && user.LockoutEnabled)
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