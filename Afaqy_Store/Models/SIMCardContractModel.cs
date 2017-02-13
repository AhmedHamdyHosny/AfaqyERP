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
    public class SIMCardContractModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSIMCardContract/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public SIMCardContractModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class SIMCardContractViewModel : SIMCardContractView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
        public string ContractDate_Format
        {
            get
            {

                return this.ContractDate != null ?  ((DateTime)this.ContractDate).ToString(Classes.Common.Constant.DateFormat) : "";
            }
        }
        public string ExpiryDate_Format
        {
            get
            {
                return this.ExpiryDate != null ? ((DateTime)this.ExpiryDate).ToString(Classes.Common.Constant.DateFormat) : "";
            }
        }
    }

    public class SIMCardContractIndexViewModel : SIMCardContractViewModel
    {

    }

    public class SIMCardContractDetailsViewModel : SIMCardContractViewModel
    {

    }

    [Bind(Include = "SIMCardContractId,SIMCardProviderId,ContractNo,CurrentCost,CurrencyId,ContractDate,ExpiryDate")]
    public class SIMCardContractCreateBindModel : SIMCardContract
    {

    }

    [Bind(Include = "SIMCardContractId,SIMCardProviderId,ContractNo,CurrentCost,CurrencyId,ContractDate,ExpiryDate,CreateUserId,CreateDate,IsBlock")]
    public class SIMCardContractEditBindModel : SIMCardContract
    {
    }
    public class SIMCardContractEditModel
    {
        public SIMCardContract EditItem { get; set; }
        public IEnumerable<SelectListItem> SIMCardProvider { get; set; }
        public IEnumerable<SelectListItem> Currency { get; set; }
    }
}