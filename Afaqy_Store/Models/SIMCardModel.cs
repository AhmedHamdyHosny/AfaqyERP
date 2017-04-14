using Afaqy_Store.DataLayer;
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

    public class SIMCardViewModel : SIMCardView
    {
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

    }

    public class SIMCardIndexViewModel : SIMCardView
    {
        //public string PurchaseDate_Format
        //{
        //    get
        //    {

        //        return this.PurchaseDate != null ? ((DateTime)this.PurchaseDate).ToString(Classes.Common.Constant.DateFormat) : "";
        //    }
        //}
        
    }

    public class SIMCardDetailsViewModel : SIMCardViewModel
    {
        
    }

    [Bind(Include = "SIMCardId,CompanySerialNumber,SerialNumber,GSM,AlternativeGSM")]
    public class SIMCardCreateBindModel : SIMCard
    {

    }

    [Bind(Include = "SIMCardId,CompanySerialNumber,SerialNumber,GSM,AlternativeGSM,SIMCardStatusId,ContractId,PurchaseDate,Warehouse_wa_code,IsBlock,CreateUserId,CreateDate")]
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