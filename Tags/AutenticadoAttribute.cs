using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EvaluacionServicios.Tags
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession())
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }
    }

    // Si estamos logeado ya no podemos acceder a la página de Login
    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {


                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Escritorio",
                    action = "Index"
                }));
            }
        }
    }
    //En caso de que se ingrese a una pagina con el link, verifica si tiene permiso, de lo contrario redirecciona al escritorio 
    public class AutenticadoPages : ActionFilterAttribute  
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (EvaluacionServicios.Models.Common.FrontUser.Get().Rol_id != 363)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Escritorio",
                    action = "Index"
                }));
            }
        }
    }
}