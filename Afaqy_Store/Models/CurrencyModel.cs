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
    public class CurrencyModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCurrency/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CurrencyModel() : base(ApiUrl, ApiRoute)
        {
        }
    }


    [Bind(Include = "CurrencyId,CurrencyName_en,CurrencyName_ar")]
    public class CurrencyCreateBindModel : Currency
    {

    }

    [Bind(Include = "CurrencyId,CurrencyName_en,CurrencyName_ar")]
    public class CurrencyEditBindModel : Currency
    {
    }
    public class CurrencyEditModel
    {
        public Currency EditItem { get; set; }
    }
}