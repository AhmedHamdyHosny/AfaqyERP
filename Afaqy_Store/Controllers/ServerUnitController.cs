using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class ServerUnitController : BaseController<ServerUnit,ServerUnitViewModel,ServerUnitIndexViewModel,ServerUnitDetailsViewModel,ServerUnit,ServerUnit,ServerUnit,ServerUnit,ServerUnitModel<ServerUnit>,ServerUnitModel<ServerUnit>>
    {
        public ActionResult Synchronize()
        {
            if (ServerUnitModel<CustServerUnit>.SynchronizeServerUnits())
            {
                TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, Transaction = Classes.Common.Enums.Transactions.Import };
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Error, Transaction = Classes.Common.Enums.Transactions.Import };
            return RedirectToAction("Index");

        }
        
    }
}