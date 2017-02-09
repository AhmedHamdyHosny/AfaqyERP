﻿using Afaqy_Store.DataLayer;
using Classes.Utilities;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using Models;
using Classes.Helper;

namespace Afaqy_Store.Models
{
    public class SIMCardModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSIMCard/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public SIMCardModel() : base(ApiUrl, ApiRoute)
        {
        }
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
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
        public string PurchaseDate_Format
        {
            get
            {

                return this.PurchaseDate != null ? ((DateTime)this.PurchaseDate).ToString(Classes.Common.Constant.DateFormat) : "";
            }
        }
        internal void BindCreate_Modify_User()
        {
            var tempUser = this.CreateUser;
        }


    }

    public class SIMCardIndexViewModel : SIMCard
    {
        public string PurchaseDate_Format
        {
            get
            {

                return this.PurchaseDate != null ? ((DateTime)this.PurchaseDate).ToString(Classes.Common.Constant.DateFormat) : "";
            }
        }
        
    }

    public class SIMCardDetailsViewModel : SIMCardViewModel
    {
        
    }

    [Bind(Include = "SIMCardId,CompanySerialNumber,SerialNumber,GSM,AlternativeGSM")]
    public class SIMCardCreateBindModel : SIMCard
    {

    }

    [Bind(Include = "SIMCardId,CompanySerialNumber,SerialNumber,GSM,AlternativeGSM,SIMCardStatusId,ContractId,PurchaseDate,BranchId,IsBlock,CreateUserId,CreateDate")]
    public class SIMCardEditBindModel : SIMCard
    {
    }

    public class SIMCardEditModel
    {
        public SIMCard EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Contract { get; set; }
    }

    public class SIMCardImportModel : SIMCard
    {
        public string ContractNo { get; set; }
    }
}