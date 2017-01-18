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
    public class DeviceModelTypeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeviceModelType/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public DeviceModelTypeModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    [Bind(Include = "ModelTypeId,ModelTypeName")]
    public class DeviceModelTypeCreateBindModel : DeviceModelType
    {
    }

    [Bind(Include = "ModelTypeId,ModelTypeName")]
    public class DeviceModelTypeEditBindModel : DeviceModelType
    {
    }
    
    public class DeviceModelTypeEditModel
    {
        public DeviceModelType EditItem { get; set; }
    }
}