using LibraryManager.Core.Entities;
using LibraryManager.App.ExtentionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManager.App.ActionFilters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObject<Member>("loggedMember") == null)
            {
                if (context.HttpContext.Session.GetObject<Member>("loggedAdmin") != null)
                {
                    context.HttpContext.Session.SetObject<Member>("loggedAdmin", null);

                }
                context.Result = new RedirectResult("/Home/Login");
            }

        }
    }
}
