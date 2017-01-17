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
    public class SIMCardStatusModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSIMCardStatus/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public SIMCardStatusModel() : base(ApiUrl, ApiRoute)
        {
            
        }
    }
    

    [Bind(Include = "SIMCardStatusId,SIMCardStatusName_en,SIMCardStatusName_ar")]
    public class SIMCardStatusEditBindModel : SIMCardStatus
    {
    }

    public class SIMCardStatusEditModel
    {
        public SIMCardStatus EditItem { get; set; }
    }
}