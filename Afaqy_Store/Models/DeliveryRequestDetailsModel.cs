using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeliveryRequestDetailsModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryRequestDetails/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryRequestDetailsModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class RequestDetails_ViewModel : DeliveryRequestDetailsView
    {
        
    }

    public class RequestDetails_DetailsViewModel : RequestDetails_ViewModel
    {

    }
}