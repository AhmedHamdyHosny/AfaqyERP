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
    public class CustomerServerStatusModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerServerStatus/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CustomerServerStatusModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerServerStatusViewModel : CustomerServerStatus
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

    public class CustomerServerStatusIndexViewModel : CustomerServerStatusViewModel
    {
    }

    public class CustomerServerStatusDetailsViewModel : CustomerServerStatusViewModel
    {
    }

    [Bind(Include = "CustomerServerStatusId,CustomerServerStatus_en,CustomerServerStatus_ar")]
    public class CustomerServerStatusCreateBindModel : CustomerServerStatus
    {

    }

    [Bind(Include = "CustomerServerStatusId,CustomerServerStatus_en,CustomerServerStatus_ar,IsBlock,CreateUserId,CreateDate")]
    public class CustomerServerStatusEditBindModel : CustomerServerStatus
    {
    }
    public class CustomerServerStatusEditModel
    {
        public CustomerServerStatus EditItem { get; set; }
    }
}