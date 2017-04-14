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
    public class CustomerContactModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomerContact/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public CustomerContactModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    //public class CustomerContactViewModel : CustomerContact
    //{
    //    private UserViewModel _createUser = null;
    //    private UserViewModel _modifyUser = null;
    //    public UserViewModel CreateUser
    //    {
    //        get
    //        {
    //            if (_createUser == null)
    //            {
    //                new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, this.ModifyUserId, ref this._createUser, ref this._modifyUser);
    //            }
    //            return _createUser;
    //        }
    //        set
    //        {
    //            _createUser = value;
    //        }
    //    }
    //    public UserViewModel ModifyUser
    //    {
    //        get
    //        {
    //            return _modifyUser;
    //        }
    //        set
    //        {
    //            _modifyUser = value;
    //        }
    //    }
    //    public string Block
    //    {
    //        get
    //        {
    //            return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
    //        }
    //    }
    //    internal void BindCreate_Modify_User()
    //    {
    //        var tempUser = this.CreateUser;
    //    }
    //}
    //public class CustomerContactIndexViewModel : CustomerContact
    //{

    //}
    //public class CustomerContactDetailsViewModel : CustomerContactViewModel
    //{

    //}

    //[Bind(Include = "CustomerContactId,DolphinId,CustomerId,ContactName_en,ContactName_ar,Position_en,Position_ar,IsDefault,CustomerContactDetails")]
    //public class CustomerContactCreateBindModel : CustomerContact
    //{

    //}

    //[Bind(Include = "CustomerContactId,DolphinId,CustomerId,ContactName_en,ContactName_ar,Position_en,Position_ar,IsDefault,CustomerContactDetails,IsBlock,CreateUserId,CreateDate")]
    //public class CustomerContactEditBindModel : CustomerContact
    //{
    //}
    //public class CustomerContactEditModel
    //{
    //    public CustomerContact EditItem { get; set; }
    //}
}