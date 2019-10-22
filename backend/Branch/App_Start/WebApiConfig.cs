using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Branch
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var Cors = new EnableCorsAttribute("*", "*", "*");
            // Serviços e configuração da API da Web
            config.EnableCors(Cors);

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
