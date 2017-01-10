using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Afaqy_Store.Models
{
    public class SIMCardStatusModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSIMCardStatus/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public SIMCardStatusModel() : base(ApiUrl, ApiRoute)
        {
            
        }
    }
}