using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using Classes.Common;
using Classes.Utilities;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Classes.Common.Enums;

namespace Afaqy_Store.Controllers
{
    public class TestSIMCardController : BaseController<SIMCard,SIMCardViewModel,SIMCardEditModel, SIMCardModel<SIMCard>, SIMCardModel<SIMCardViewModel>>
    {
        public TestSIMCardController()
        {
            IndexRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardId", Operation = GenericDataFormat.FilterOperations.Equal});
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "SIMCardId,SerialNumber,GSM,SIMCardStatus.SIMCardStatusName_en", References = "SIMCardStatus" } };
            References = new List<Reference>();
            References.Add(new Reference() { TypeModel = typeof(SIMCardStatusModel<SIMCardViewModel>), ViewDataName = "SIMCardStatusId", DataValueField = "SIMCardStatusId", DataTextField = "SIMCardStatusName_en", SelectColumns = "SIMCardStatusId,SIMCardStatusName_en" });
            ExcelFileName = "SIMCards.xlsx";
        }

        // POST: SIMCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SIMCardId,SerialNumber,GSM,SIMCardStatusId")] SIMCard model)
        {
            if (ModelState.IsValid)
            {
                //for test
                model.CreateUserId = 1;
                model.CreateDate = DateTime.Now;

                new SIMCardModel<SIMCard>().Insert(model);
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
                return RedirectToAction("Index");
            }

            var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();
            ViewBag.SIMCardStatusId = new SelectList(SIMCardStatus, "SIMCardStatusId", "SIMCardStatusName_en", model.SIMCardStatusId);
            return View(model);
        }

        // GET: SIMCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIMCard SimCard = new SIMCardModel<SIMCard>().Get((int)id);
            if (SimCard == null)
            {
                return HttpNotFound();
            }

            var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();

            var model = new SIMCardEditModel()
            {
                EditItem = SimCard,
                Status = SIMCardStatus.Select(x => new SelectListItem() { Selected = SimCard.SIMCardStatusId == x.SIMCardStatusId, Text = x.SIMCardStatusName_en, Value = x.SIMCardStatusId.ToString() })
            };

            return View(model);
        }

        // POST: SIMCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SIMCardId,SerialNumber,GSM,SIMCardStatusId,CreateUserId,CreateDate")] SIMCard EditItem)
        {
            if (ModelState.IsValid)
            {
                //for test
                EditItem.ModifyUserId = 1;
                EditItem.ModifyDate = DateTime.Now;

                new SIMCardModel<SIMCard>().Update(EditItem, EditItem.SIMCardId);
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
                return RedirectToAction("Index");
            }
            var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();
            var model = new SIMCardEditModel()
            {
                EditItem = new SIMCardModel<SIMCard>().Get(EditItem.SIMCardId),
                Status = SIMCardStatus.Select(x => new SelectListItem() { Selected = EditItem.SIMCardStatusId == x.SIMCardStatusId, Text = x.SIMCardStatusName_en, Value = x.SIMCardStatusId.ToString() })
            };
            return View(model);
        }

    }
}