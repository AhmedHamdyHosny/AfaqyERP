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
    public class BrandServerPortModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiBrandServerPort/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public BrandServerPortModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class BrandServerPortViewModel : BrandServerPortView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    public class BrandServerPortIndexViewModel : BrandServerPortView
    {

    }
    public class BrandServerPortDetailsViewModel : BrandServerPortViewModel
    {

    }

    [Bind(Include = "BrandPortId,SystemServerId,BrandId,PortNumber")]
    public class BrandServerPortCreateBindModel : BrandServerPort
    {

    }

    [Bind(Include = "BrandPortId,SystemServerId,BrandId,PortNumber,IsBlock,CreateUserId,CreateDate")]
    public class BrandServerPortEditBindModel : BrandServerPort
    {
    }
    public class BrandServerPortEditModel
    {
        public BrandServerPort EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Brand { get; set; }
        public IEnumerable<CustomSelectListItem> SystemServerIP { get; set; }
    }
}