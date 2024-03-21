using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PracticaMvcCore2Iniciales.Filters
{
    public class AuthorizeUsuariosAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                //ENVIAMOS A LA VISTA LOGIN
                context.Result = this.GetRoute("Managed", "Login");
            }

        }

        //COMO TENDREMOS MULTIPLES REDIRECCIONES, CREAMOS UN METODO
        //PARA FACILITAR LA REDIRECCION
        private RedirectToRouteResult GetRoute
            (string controller, string action)
        {
            RouteValueDictionary ruta =
                new RouteValueDictionary(
                    new { controller = controller, action = action });
            RedirectToRouteResult result =
                new RedirectToRouteResult(ruta);
            return result;
        }


    }
}
