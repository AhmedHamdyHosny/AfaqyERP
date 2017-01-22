using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class CountryModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCountry/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CountryModel() : base(ApiUrl, ApiRoute)
        {
        }
    }
    
}