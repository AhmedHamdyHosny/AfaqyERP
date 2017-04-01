using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeliveryDeviceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryDevice/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryDeviceModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryDeviceViewModel : DeliveryDeviceView
    {

    }

    public class DeliveryDeviceDetailsViewModel : DeliveryDeviceViewModel
    {

    }

    
}