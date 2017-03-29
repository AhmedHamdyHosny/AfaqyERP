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
    public class WarehouseModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiWarehouse/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public WarehouseModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class WarehouseViewModel : Warehouse
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
    public class WarehouseIndexViewModel : Warehouse
    {

    }
    public class WarehouseDetailsViewModel : WarehouseViewModel
    {

    }
    [Bind(Include = "WarehouseId,WarehouseName_en,WarehouseName_ar,BranchId")]
    public class WarehouseCreateBindModel : Warehouse
    {

    }
    [Bind(Include = "WarehouseId,WarehouseName_en,WarehouseName_ar,BranchId,IsBlock,CreateUserId,CreateDate")]
    public class WarehouseEditBindModel : Warehouse
    {
    }
    public class WarehouseEditModel
    {
        public Warehouse EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Branch { get; set; }
    }
}