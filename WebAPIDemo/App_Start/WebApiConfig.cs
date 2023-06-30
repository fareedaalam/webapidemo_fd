using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebAPIDemo.Areas.HelpPage;

namespace WebAPIDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {    //Suggested by abhay
            // config.MessageHandlers.Add(new CrossDomainHandler());

            // config.EnableCors(new EnableCorsAttribute("http://localhost:4200", headers: "*", methods: "*"));

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //Enable cors
            //Get AppSetting Key      

            //string j = ConfigurationManager.AppSettings["AllowOrigonDomain"].ToString();
            //string k = ConfigurationManager.AppSettings["AllowHeaders"].ToString();
            //string l = ConfigurationManager.AppSettings["AllowMethood"].ToString();

            var cors = new EnableCorsAttribute(ConfigurationManager.AppSettings["AllowOrigonDomain"].ToString(),
                                              ConfigurationManager.AppSettings["AllowHeaders"].ToString(),
                                              ConfigurationManager.AppSettings["AllowMethood"].ToString());

            // var cors = new EnableCorsAttribute("http://localhost:4200,http://localhost:50458", "*", "*");
            // var cors = new EnableCorsAttribute("*","*", "*");

             config.EnableCors(cors);
           // config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //For show Api description on web page
            config.SetDocumentationProvider(new XmlDocumentationProvider(
              HttpContext.Current.Server.MapPath("~/bin/WebAPIDemo.xml")));
           // config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/MvcApplication4.XML")));


        }
    }
}
