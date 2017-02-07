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
    public class CustomerServerIPModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerServerIP/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CustomerServerIPModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerServerIPViewModel : CustomerServerIP
    {

        private UserViewModel _createUser = null;
        private UserViewModel _modifyUser = null;
        public UserViewModel CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, this.ModifyUserId, ref this._createUser, ref this._modifyUser);
                }
                return _createUser;
            }
            set
            {
                _createUser = value;
            }
        }
        public UserViewModel ModifyUser
        {
            get
            {
                return _modifyUser;
            }
            set
            {
                _modifyUser = value;
            }
        }
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
        internal void BindCreate_Modify_User()
        {
            var tempUser = this.CreateUser;
        }

    }

    public class CustomerServerIPIndexViewModel : CustomerServerIPViewModel
    {
    }

    public class CustomerServerIPDetailsViewModel : CustomerServerIPViewModel
    {
    }

    [Bind(Include = "CustomerServerId,CustomerId,SystemServerId,AccountUserName,AccountPassword")]
    public class CustomerServerIPCreateBindModel : CustomerServerIP
    {

    }

    [Bind(Include = "CustomerServerId,CustomerId,SystemServerId,AccountUserName,AccountPassword,IsBlock,CreateUserId,CreateDate")]
    public class CustomerServerIPEditBindModel : CustomerServerIP
    {
    }
    public class CustomerServerIPEditModel
    {
        public CustomerServerIP EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> SystemServerIP { get; set; }
    }
}