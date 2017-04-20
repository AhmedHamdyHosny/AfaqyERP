using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class NotificationModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiNotification/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public NotificationModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class NotificationViewModel : Notification
    {
       
    }
    
    public class NotificationDetailsViewModel : DeliveryNoteViewModel
    {
    }
}