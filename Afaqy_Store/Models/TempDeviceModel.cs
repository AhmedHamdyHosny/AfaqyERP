using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class TempDeviceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiTempDevice/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public TempDeviceModel() : base(ApiUrl, ApiRoute)
        {
        }
    }
}