using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class SiteConfig
    {
        private static string _ApiUrl;
        public static string ApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_ApiUrl))
                    _ApiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
                return _ApiUrl;
            }
        }
    }
}