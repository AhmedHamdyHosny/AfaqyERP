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
    public class CustomerModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomer/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CustomerModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerViewModel : Customer
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

    public class CustomerIndexViewModel : CustomerViewModel
    {
    }

    public class CustomerDetailsViewModel : CustomerViewModel
    {
    }

    [Bind(Include = "CustomerId,CustomerName_en,CustomerName_ar,ContactName,ContactTelephone,Email")]
    public class CustomerCreateBindModel : Customer
    {

    }

    [Bind(Include = "CustomerId,CustomerName_en,CustomerName_ar,ContactName,ContactTelephone,Email,IsBlock,CreateUserId,CreateDate")]
    public class CustomerEditBindModel : Customer
    {
    }
    public class CustomerEditModel
    {
        public Customer EditItem { get; set; }
    }
}