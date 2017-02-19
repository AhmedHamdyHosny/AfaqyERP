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
    public class BrandModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiBrand/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public BrandModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class BrandViewModel : Brand
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
    public class BrandIndexViewModel : Brand
    {

    }
    public class BrandDetailsViewModel : BrandViewModel
    {

    }

    [Bind(Include = "BrandId,BrandName,ProtocolName")]
    public class BrandCreateBindModel : Brand
    {

    }

    [Bind(Include = "BrandId,BrandName,ProtocolName,IsBlock,CreateUserId,CreateDate")]
    public class BrandEditBindModel : Brand
    {
    }
    public class BrandEditModel
    {
        public Brand EditItem { get; set; }
    }
}