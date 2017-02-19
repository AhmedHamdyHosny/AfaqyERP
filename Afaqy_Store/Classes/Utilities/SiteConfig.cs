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

        public static string SecurityKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SecurityKey"];
            }
        }

        public static string ImportFilesPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImportFilesPath"];
            }
        }

        public static class AfaqyIn
        {
            public static string Url
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.In_Urls"];
                }
            }
        }

        public static class AfaqyNet
        {
            public static string Url
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.Net_Urls"];
                }
            }
        }

        public static class AfaqyInfo
        {
            public static string Url
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.Info_Url"];
                }
            }
            public static string UserName
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.Info_UserName"];
                }
            }
            public static string Password
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.Info_Password"];
                }
            }
        }

        public static class AfaqyMe
        {
            public static string Url
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.me_Url"];
                }
            }
            public static string Token
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["Afaqy.me_Token"];
                }
            }
           
        }
    }
}