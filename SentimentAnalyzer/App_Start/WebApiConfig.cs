﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using SentimentAnalyzer.Providers;

namespace SentimentAnalyzer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new UnhandledExceptionFilter());//<--Add this line
        }
    }
}
