using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeviceStatusHistoryModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeviceStatusHistory/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public DeviceStatusHistoryModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeviceStatusHistoryViewModel : DeviceStatusHistory
    {

        private UserViewModel _createUser = null;
        private UserViewModel _modifyUser = null;
        public UserViewModel CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    new UserModel<UserViewModel>().Get_Create_Modify_User((int)this.CreateUserId, null, ref this._createUser,ref this._modifyUser);
                }
                return _createUser;
            }
            set
            {
                _createUser = value;
            }
        }

        internal void BindCreate_Modify_User()
        {
            var tempUser = this.CreateUser;
        }
    }
}