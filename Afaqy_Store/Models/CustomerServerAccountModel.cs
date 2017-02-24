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
    public class CustomerServerAccountModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerServerAccount/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CustomerServerAccountModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerServerAccountViewModel : CustomerServerAccountView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }

    }

    public class CustomerServerAccountIndexViewModel : CustomerServerAccountView
    {
    }

    public class CustomerServerAccountDetailsViewModel : CustomerServerAccountViewModel
    {
    }

    [Bind(Include = "CustomerServerAccountId,CustomerId,SystemServerId,AccountUserName,AccountPassword")]
    public class CustomerServerAccountCreateBindModel : CustomerServerAccount
    {

    }

    [Bind(Include = "CustomerServerAccountId,CustomerId,SystemServerId,AccountUserName,AccountPassword,IsBlock,CreateUserId,CreateDate")]
    public class CustomerServerAccountEditBindModel : CustomerServerAccount
    {
    }
    public class CustomerServerAccountEditModel
    {
        public CustomerServerAccount EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> SystemServerIP { get; set; }
    }

    public class CustomerServerAccountImportModel : CustomerServerAccount
    {
        public string Status { get; set; }
        public string ServerIP { get; set; }
    }
}