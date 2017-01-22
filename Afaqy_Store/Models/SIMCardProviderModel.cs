using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class SIMCardProviderModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSIMCardProvider/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public SIMCardProviderModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class SIMCardProviderViewModel : SIMCardProvider
    {

        

    }

    [Bind(Include = "ProviderId,ProviderName_en,ProviderName_ar")]
    public class SIMCardProviderCreateBindModel : SIMCardProvider
    {

    }

    [Bind(Include = "ProviderId,ProviderName_en,ProviderName_ar")]
    public class SIMCardProviderEditBindModel : SIMCardProvider
    {
    }
    public class SIMCardProviderEditModel
    {
        public SIMCardProvider EditItem { get; set; }
    }
}