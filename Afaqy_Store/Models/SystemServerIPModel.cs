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
    public class SystemServerIPModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSystemServerIP/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public SystemServerIPModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class SystemServerIPViewModel : SystemServerIP
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

    [Bind(Include = "SystemServerId,ServerIP,SystemId")]
    public class SystemServerIPCreateBindModel : SystemServerIP
    {

    }

    [Bind(Include = "SystemServerId,ServerIP,SystemId,IsBlock,CreateUserId,CreateDate")]
    public class SystemServerIPEditBindModel : SystemServerIP
    {
    }
    public class SystemServerIPEditModel
    {
        public SystemServerIP EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
    }
}