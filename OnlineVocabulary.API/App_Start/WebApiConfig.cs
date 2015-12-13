using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using OnlineVocabulary.API.Routing;
using System.Web.Http.Dispatcher;
using OnlineVocabulary.API.Dispatchers;

namespace OnlineVocabulary.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            HttpMessageHandler entryTreeNodePipeline = HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new[] { new EntryTreeNodeDispatcher() });

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "EntryTreeNodeHttpRoute",
                "api/entryTree/{key}/node/{nodeKey}",
                defaults: new { controller = "EntryTreeNode", shipmentKey = RouteParameter.Optional },
                constraints: new { key = new GuidRouteConstraint(), shipmentKey = new GuidRouteConstraint() },
                handler: entryTreeNodePipeline);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
