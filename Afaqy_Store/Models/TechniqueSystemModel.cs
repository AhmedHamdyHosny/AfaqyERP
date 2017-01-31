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
    public class TechniqueSystemModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiTechniqueSystem/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public TechniqueSystemModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class TechniqueSystemViewModel : TechniqueSystem
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
        internal void BindCreate_Modify_User()
        {
            var tempUser = this.CreateUser;
        }
    }

    [Bind(Include = "SystemId,SystemName,CompanyId")]
    public class TechniqueSystemCreateBindModel : TechniqueSystem
    {

    }

    [Bind(Include = "SystemId,SystemName,CompanyId,IsBlock,CreateUserId,CreateDate")]
    public class TechniqueSystemEditBindModel : TechniqueSystem
    {
    }
    public class TechniqueSystemEditModel
    {
        public TechniqueSystem EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueCompany { get; set; }
    }
}