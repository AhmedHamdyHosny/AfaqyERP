using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class TransactionStatusHistoryModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiTransactionStatusHistory/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public TransactionStatusHistoryModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

}