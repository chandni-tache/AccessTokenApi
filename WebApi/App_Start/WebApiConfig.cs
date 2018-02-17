using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            var formatters = new List<MediaTypeFormatter>() {
            new JsonMediaTypeFormatter(),
            new XmlMediaTypeFormatter()
};
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
            // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }
    }
}