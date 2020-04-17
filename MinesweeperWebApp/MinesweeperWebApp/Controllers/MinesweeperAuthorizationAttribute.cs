using System;
using System.Web.Mvc;
using MinesweeperWebApp.Services.Utility;

namespace MinesweeperWebApp.Controllers
{
    /*
     * This class supports page-level security
     * Controller methods use the attribute [MinesweeperAuthorization] to enforce authorization
     */ 
    internal class MinesweeperAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        /*
         * This method enforces page level security
         */
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // if the user is not signed in
            if (Cache.AccessCache().Get("activeAccount") == null)
            {
                // redirect to login
                filterContext.Result = new RedirectResult("/account/showloginform");
            }
        }
    }
}