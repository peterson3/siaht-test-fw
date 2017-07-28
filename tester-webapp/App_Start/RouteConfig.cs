using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace tester_webapp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "TelaRouteIndex",
                url: "Tela/Index",
                defaults: new { controller = "Tela", action = "Index"}
                );
            routes.MapRoute(
                name: "TelaRoute",
                url: "Tela/{sistemaId}/{id}",
                defaults: new { controller = "Tela", action = "TelasSistema", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "AcaoRouteSelecaoSistema",
                url: "Acao/Index",
                defaults: new { controller = "Acao", action = "Index" }
                );
            routes.MapRoute(
                name: "AcaoRoute",
                url: "Acao/{sistemaId}/{telaId}/{acaoId}",
                defaults: new { controller = "Acao", action = "SelecionaTela", sistemaId = UrlParameter.Optional, telaId = UrlParameter.Optional, acaoId = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
