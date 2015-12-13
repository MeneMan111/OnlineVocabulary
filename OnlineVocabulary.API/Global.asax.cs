using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using OnlineVocabulary.API.Mappers;

namespace OnlineVocabulary.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.RegisterMappings();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
