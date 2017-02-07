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
    public class ServerRoleModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiServerRole/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ServerRoleModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class ServerRoleViewModel : ServerRole
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

    public class ServerRoleIndexViewModel : ServerRoleViewModel
    {
    }

    public class ServerRoleDetailsViewModel : ServerRoleViewModel
    {
    }

    [Bind(Include = "ServerRoleId,ServerRole_en,ServerRole_ar")]
    public class ServerRoleCreateBindModel : ServerRole
    {

    }

    [Bind(Include = "ServerRoleId,ServerRole_en,ServerRole_ar,IsBlock,CreateUserId,CreateDate")]
    public class ServerRoleEditBindModel : ServerRole
    {
    }
    public class ServerRoleEditModel
    {
        public ServerRole EditItem { get; set; }
    }
}