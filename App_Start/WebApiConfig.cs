using RoutingWebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace RoutingWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );

            var sourceKeyList = ConfigurationManager.AppSettings["SourceKeyList"];
            if (!string.IsNullOrEmpty(sourceKeyList))
            {
                ApplicationConfig.SourceKeyList = sourceKeyList.Split(',').ToList();
            }

            var redirectApiURL = ConfigurationManager.AppSettings["RedirectApiURL"];

            if (!string.IsNullOrEmpty(redirectApiURL))
            {
                ApplicationConfig.RedirectApiURL = redirectApiURL;
            }
        }
    }
}
