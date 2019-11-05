using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SudokuForAll.AuthData
{
    public class AuthAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private bool auth;

        public void OnAuthentication(AuthenticationContext filterContext)
        {
           // this.auth = (filterContext.ActionDescriptor.GetCustomAttributes(typeof(OverrideAuthenticationAttribute), true).Length == 0);

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
           /* var user = filterContext.HttpContext.User;
            if (System.Web.HttpContext.Current.Session["Usuario"] == null )
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }*/
        }
    }
}