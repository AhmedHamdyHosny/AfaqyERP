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
    public class SystemServerIPModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSystemServerIP/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public SystemServerIPModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class SystemServerIPViewModel : SystemServerIPView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    public class SystemServerIPIndexViewModel : SystemServerIPView
    {

    }
    public class SystemServerIPDetailsViewModel : SystemServerIPViewModel
    {

    }

    [Bind(Include = "SystemServerId,ServerIP,SystemId")]
    public class SystemServerIPCreateBindModel : SystemServerIP
    {

    }

    [Bind(Include = "SystemServerId,ServerIP,SystemId,IsBlock,CreateUserId,CreateDate")]
    public class SystemServerIPEditBindModel : SystemServerIP
    {
    }
    public class SystemServerIPEditModel
    {
        public SystemServerIP EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
    }
}