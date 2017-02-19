using Afaqy_Store.Models;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class AfaqyTrackersController : Controller
    {
        public JsonResult GetView(GenericDataFormat options)
        {
            List<ServerUnit> result = ServerUnitModel.GetAllUnits(options);
            var model = new PaginationResult<ServerUnit> { TotalItemsCount = result.Count, PageItems = result };
            return Json(model);
        }

        public ActionResult Index()
        {
            //var model = Unit.GetAllUnits();
            return View();
        }
    }
}