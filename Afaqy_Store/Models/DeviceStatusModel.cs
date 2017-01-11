using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeviceStatusModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeviceStatus/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public DeviceStatusModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeviceStatusViewModel : DeviceStatus
    {

    }

    public class DeviceStatusEditModel
    {
        public DeviceStatus EditItem { get; set; }
    }
}