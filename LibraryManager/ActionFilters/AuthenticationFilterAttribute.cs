using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManager.ActionFilters
{
    public class AuthenticationFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetObject<Member>("loggedMember") == null)
            {
                context.Result = new RedirectResult("/Home/Login");
            } 
        }
    }
}
