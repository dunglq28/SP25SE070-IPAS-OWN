using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CapstoneProject_SP25_IPAS_API.ProgramConfig.AuthorizeConfig
{
    public class CustomeAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _role;

        public CustomeAuthorizeAttribute(string[] requiredRoles)
        {
            _role = requiredRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
        }
    }
}
