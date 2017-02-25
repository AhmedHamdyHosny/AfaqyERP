using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class CustomerServerUnitsModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerServerUnitsView/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public CustomerServerUnitsModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerServerUnitsCountModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerServerUnitsCountView/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public CustomerServerUnitsCountModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerServerUnitsCountViewModel : CustomerServerUnitsCountView
    {
        
    }

    public class CustomerServerUnitsCountIndexViewModel : CustomerServerUnitsCountView
    {

    }
    public class CustomerServerUnitsDetailsViewModel : CustomerServerUnitsView
    {
        public string LastConnection_Format
        {
            get
            {
                return this.LastConnection != null ? ((DateTime)this.LastConnection).ToString(Classes.Common.Constant.DateTimeFormat) : "";
            }
        }
    }
}