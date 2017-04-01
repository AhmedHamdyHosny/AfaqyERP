using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeliveryRequestTechnicianModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryRequestTechnician/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryRequestTechnicianModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryRequestTechnicianViewModel : DeliveryRequestTechnicianView
    {
       
    }
}