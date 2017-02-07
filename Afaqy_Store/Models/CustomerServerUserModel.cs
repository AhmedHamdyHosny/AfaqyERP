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
    public class CustomerServerUserModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerServerUser/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CustomerServerUserModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerServerUserViewModel : CustomerServerUser
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

    public class CustomerServerUserIndexViewModel : CustomerServerUserViewModel
    {
    }

    public class CustomerServerUserDetailsViewModel : CustomerServerUserViewModel
    {
    }

    [Bind(Include = "CustomerServerUserId,CustomerId,SystemServerId,AccountUserName,AccountPassword")]
    public class CustomerServerUserCreateBindModel : CustomerServerUser
    {

    }

    [Bind(Include = "CustomerServerUserId,CustomerId,SystemServerId,AccountUserName,AccountPassword,IsBlock,CreateUserId,CreateDate")]
    public class CustomerServerUserEditBindModel : CustomerServerUser
    {
    }
    public class CustomerServerUserEditModel
    {
        public CustomerServerUser EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> SystemServerIP { get; set; }
    }
}