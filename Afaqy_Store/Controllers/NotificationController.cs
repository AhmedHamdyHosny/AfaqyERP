using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class NotificationController : BaseController<Notification,NotificationViewModel,NotificationIndexViewModel, NotificationDetailsViewModel, NotificationCreateBindModel, NotificationEditBindModel, Notification, Notification, NotificationModel<Notification>, NotificationModel<NotificationViewModel>>
    {
        #region Override Actions
        [NonAction]
        public override ActionResult Details(object id)
        {
            return null;
        }
        [NonAction]
        public override ActionResult Create()
        {
            return null;
        }
        [NonAction]
        public override ActionResult Create(NotificationCreateBindModel model)
        {
            return base.Create(model);
        }

        [NonAction]
        public override ActionResult Edit(object id)
        {
            return null;
        }
        
        [NonAction]
        public override ActionResult Edit(NotificationEditBindModel EditItem)
        {
            return base.Edit(EditItem);
        }
        #endregion
    }
}