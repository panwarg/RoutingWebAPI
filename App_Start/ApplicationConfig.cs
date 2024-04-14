using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoutingWebAPI.App_Start
{
    public static class ApplicationConfig
    {
        public static IEnumerable<string> SourceKeyList { get; set; }

        public static string RedirectApiURL { get; set;}
    }
}