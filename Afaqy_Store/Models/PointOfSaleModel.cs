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
    public class PointOfSaleModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiPointOfSale/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public PointOfSaleModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    //public class PointOfSaleViewModel : PointOfSale
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
    //public class PointOfSaleIndexViewModel : PointOfSale
    //{

    //}
    //public class PointOfSaleDetailsViewModel : PointOfSaleViewModel
    //{

    //}
    //[Bind(Include = "POSId,POSName_en,POSName_ar,BranchId")]
    //public class PointOfSaleCreateBindModel : PointOfSale
    //{

    //}
    //[Bind(Include = "POSId,POSName_en,POSName_ar,BranchId,IsBlock,CreateUserId,CreateDate")]
    //public class PointOfSaleEditBindModel : PointOfSale
    //{
    //}
    //public class PointOfSaleEditModel
    //{
    //    public PointOfSale EditItem { get; set; }
    //    public IEnumerable<CustomSelectListItem> Branch { get; set; }
    //}
}