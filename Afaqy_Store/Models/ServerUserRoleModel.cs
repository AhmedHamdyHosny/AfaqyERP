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
    public class ServerUserRoleModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSeverUserRole/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ServerUserRoleModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class ServerUserRoleViewModel : ServerUserRole
    {

        private UserViewModel _createUser = null;
        private UserViewModel _modifyUser = null;
        public UserViewModel CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, null, ref this._createUser, ref this._modifyUser);
                }
                return _createUser;
            }
            set
            {
                _createUser = value;
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

    public class ServerUserRoleIndexViewModel : ServerUserRoleViewModel
    {
    }

    public class ServerUserRoleDetailsViewModel : ServerUserRoleViewModel
    {
    }

    [Bind(Include = "ServerUserRoleId,CustomerServerUserId,ServerRoleId")]
    public class ServerUserRoleCreateBindModel : ServerUserRole
    {

    }

    [Bind(Include = "ServerUserRoleId,CustomerServerUserId,ServerRoleId,IsBlock,CreateUserId,CreateDate")]
    public class ServerUserRoleEditBindModel : ServerUserRole
    {
    }
    public class ServerUserRoleEditModel
    {
        public ServerUserRole EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> CustomerServerUser { get; set; }
        public IEnumerable<CustomSelectListItem> ServerRole { get; set; }
        
    }
}