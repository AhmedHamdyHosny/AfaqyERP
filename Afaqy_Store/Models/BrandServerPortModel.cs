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

    public class BrandServerPortViewModel : BrandServerPort
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