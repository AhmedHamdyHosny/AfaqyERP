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
    public class DeviceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDevice/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public DeviceModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeviceViewModel : Device
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

    [Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId")]
    public class DeviceCreateBindModel : Device
    {

    }

    [Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId,DeviceStatusId,CreateUserId,CreateDate")]
    public class DeviceEditBindModel : Device
    {
    }
    public class DeviceEditModel 
    {
        public Device EditItem { get; set; }
        public IEnumerable<SelectListItem> ModelType { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}