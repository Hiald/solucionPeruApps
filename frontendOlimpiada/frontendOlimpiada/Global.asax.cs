using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace frontendOlimpiada
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string wsRouteOlimpiadaBE = System.Web.Configuration.WebConfigurationManager.AppSettings["routews"].ToString();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Response.Clear();
            var httpException = exception as HttpException;
            HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(new RequestContext(httpContext, new RouteData()));
            int CodigoError = (httpException == null ? 500 : httpException.GetHttpCode());
            string redirectUrl = urlHelper.Action("login", "usuario", new { Error = CodigoError });
            httpContext.Response.Redirect(redirectUrl, true);
        }
    }
}
