using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeliveryDetailsModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryDetails/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryDetailsModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryDetailsViewModel : TransactionDetailsView
    {

    }

    public class DeliveryDetails_DetailsViewModel : TransactionDetailsView
    {

    }
}