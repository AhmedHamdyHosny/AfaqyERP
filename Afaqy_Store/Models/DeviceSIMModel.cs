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
    public class DeviceSIMModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeviceSIM/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeviceSIMModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeviceSIMViewModel : DeviceSIMView
    {
        
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    
    }

    public class DeviceSIMIndexViewModel : DeviceSIMView
    {

    }
    public class DeviceSIMDetailsViewModel : DeviceSIMViewModel
    {

    }

    [Bind(Include = "DeviceSIMId,DeviceId,SIMCardId")]
    public class DeviceSIMCreateBindModel : DeviceSIM
    {

    }

    [Bind(Include = "DeviceSIMId,DeviceId,SIMCardId,IsBlock,CreateUserId,CreateDate")]
    public class DeviceSIMEditBindModel : DeviceSIM
    {
    }
    public class DeviceSIMEditModel
    {
        public DeviceSIM EditItem { get; set; }
        //public IEnumerable<Classes.Helper.CustomSelectListItem> Device { get; set; }
        //public IEnumerable<Classes.Helper.CustomSelectListItem> SIMCard { get; set; }
    }
}