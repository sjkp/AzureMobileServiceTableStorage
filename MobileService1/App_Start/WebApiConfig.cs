﻿using System.Configuration;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace MobileService1
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
           
            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            var constring = ConfigurationManager.AppSettings["AzureTable"];                
           
            new ApiServices(config).Settings.Connections.Add("AzureTable", new ConnectionSettings("AzureTable", constring));
        }
    }
}

