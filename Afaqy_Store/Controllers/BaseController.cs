using Afaqy_Store.Models;
using Classes.Common;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class BaseController<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel> : GenericContoller<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel>
    {
        public UserViewModel User = new UserViewModel().GetUserFromSession();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(User != null)
            {
                //get notification
                var filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "ToUserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = User.UserId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsRead", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                var requestBody = new GenericDataFormat() { Filters = filters };
                List<NotificationViewModel> notifications = new NotificationModel<NotificationViewModel>().GetView<NotificationViewModel>(requestBody).PageItems;
                ViewBag.NotificationCount = notifications.Count();
                ViewBag.Notifications = notifications;
                base.OnActionExecuting(filterContext);
            }
            
        }
    }
}