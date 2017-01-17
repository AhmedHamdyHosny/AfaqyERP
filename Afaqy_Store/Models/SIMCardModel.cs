using Afaqy_Store.DataLayer;
using Classes.Utilities;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using Models;

namespace Afaqy_Store.Models
{
    public class SIMCardModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSIMCard/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public SIMCardModel() : base(ApiUrl, ApiRoute)
        {
        }

        //public override TModel Insert(TModel obj)
        //{
        //    return base.Insert(obj);
        //}

        //public override TModel Update(TModel obj, int id)
        //{
        //    return base.Update(obj, id);
        //}
    }

    public class SIMCardViewModel : SIMCard
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
            var tempUser =  this.CreateUser;
        }

        //[Display(Name = "Status")]
        //public Enums.Status ItemStatus
        //{
        //    get
        //    {
        //        if (Status != null)
        //        {
        //            return (Enums.Status)Status;
        //        }
        //        else
        //        {
        //            return Enums.Status.None;
        //        }

        //    }
        //    set
        //    {
        //        ItemStatus = value;
        //    }
        //}


    }

    [Bind(Include = "SIMCardId,SerialNumber,GSM")]
    public class SIMCardCreateBindModel : SIMCard
    {

    }

    [Bind(Include = "SIMCardId,SerialNumber,GSM,SIMCardStatusId,CreateUserId,CreateDate")]
    public class SIMCardEditBindModel : SIMCard
    {
    }

    public class SIMCardEditModel
    {
        public SIMCard EditItem { get; set; }
        //public IEnumerable<SelectListItem> Status { get; set; }
    }
}