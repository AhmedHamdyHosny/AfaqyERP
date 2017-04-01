using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class DeliveryTechnicianModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryTechnician/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryTechnicianModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryTechnicianViewModel : DeliveryTechnician
    {

    }

    public class DeliveryTechnicianDetailsViewModel : DeliveryTechnicianViewModel
    {

    }
}