using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace WebAPIDemo.Filters
{
    public class ApiAuthenticationFilter: GenericAuthenticationFilter
    {
        public ApiAuthenticationFilter()
        {
        }
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }
        // Protected overriden method for authorizing user
        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration
                               .DependencyResolver.GetService(typeof(IUserInterface)) as IUserInterface;
            if (provider != null)
            {
                var userId = provider.Authenticate(username, password);
                if (userId !=null)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.UserId = userId;
                    return true;
                }
            }
            return false;
        }
    }
}