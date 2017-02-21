using Afaqy_Store.DataLayer;
using Classes.Helper;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class DeviceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDevice/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public DeviceModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeviceViewModel : DeviceView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }

    }

    public class DeviceIndexViewModel : DeviceView
    {
        
    }

    public class DeviceDetailsViewModel : DeviceView
    {

    }

    [Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId")]
    public class DeviceCreateBindModel : Device
    {

    }

    [Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId,DeviceStatusId,BranchId,IsBlock,CreateUserId,CreateDate")]
    public class DeviceEditBindModel : Device
    {
    }
    public class DeviceEditModel 
    {
        public Device EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> ModelType { get; set; }
    }

    public class DeviceImportModel:Device
    {
        public string DeviceSerial { get; set; }
    }
}