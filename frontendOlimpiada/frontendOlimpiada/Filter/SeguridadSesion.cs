using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using OlimpiadaUtil;

namespace frontendOlimpiada.Filter
{
    public class SecuritySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();

            if (bValidar)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "usuario", action = "login" }));

            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SeguridadSessionAjax : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();

            if (bValidar)
            {

                filterContext.Result = new JsonResult
                {
                    Data = new { iTipoResultado = -5, message = "error" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };

            }
            base.OnActionExecuting(filterContext);
        }
    }

}