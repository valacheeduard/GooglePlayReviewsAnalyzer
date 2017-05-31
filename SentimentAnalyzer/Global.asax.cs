using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Elmah.Contrib.WebApi;
using Newtonsoft.Json.Serialization;

namespace SentimentAnalyzer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            GlobalConfiguration.Configuration
               .Formatters
               .JsonFormatter
               .SerializerSettings
               .ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
