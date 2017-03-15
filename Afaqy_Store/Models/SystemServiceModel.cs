using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Security.Models
{
    public class SystemServiceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSystemService/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public SystemServiceModel() : base(ApiUrl, ApiRoute)
        {
        }
    }
}