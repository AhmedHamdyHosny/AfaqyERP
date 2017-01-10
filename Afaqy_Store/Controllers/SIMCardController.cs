using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using GenericApiController.Utilities;
using static Classes.Common.Enums;
using Classes.Utilities;

namespace Afaqy_Store.Controllers
{
    public class SIMCardController : Controller
    {
        private GenericDataFormat requestBody;
        

        public ActionResult TestDesign()
        {
            requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };

            var model = new SIMCardModel<SIMCardViewModel>().Get(requestBody);
            return View(model);
        }

        // GET: SIMCard
        public ActionResult Index()
        {
            if(TempData["AlertMessage"] != null)
            {
                ViewBag.AlertMessage = TempData["AlertMessage"];
            }
            requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            var model = new SIMCardModel<SIMCardViewModel>().Get(requestBody);
            return View(model);
        }

        // GET: SIMCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            var items = new SIMCardModel<SIMCardViewModel>().Get(requestBody);

            if (items.Count < 1 || items.ElementAt(0) == null)
            {
                return HttpNotFound();
            }

            var model = (SIMCardViewModel)items.ElementAt(0);
            model.BindCreate_Modify_User();

            return View(model);
        }

        // GET: SIMCard/Create
        public ActionResult Create()
        {
            var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();
            ViewBag.SIMCardStatusId = new SelectList(SIMCardStatus, "SIMCardStatusId", "SIMCardStatusName_en");
            return View();
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
            SIMCard SimCard = new SIMCardModel<SIMCard>().Get((int)id) ;
            if (SimCard == null)
            {
                return HttpNotFound();
            }

            var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();
            
            var model = new SIMCardEditModel()
            {
                EditItem = SimCard,
                Status = SIMCardStatus.Select(x=> new SelectListItem() { Selected = SimCard.SIMCardStatusId == x.SIMCardStatusId , Text = x.SIMCardStatusName_en , Value = x.SIMCardStatusId.ToString()})
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
                EditItem = new SIMCardModel<SIMCard>().Get(EditItem.SIMCardId) ,
                Status = SIMCardStatus.Select(x => new SelectListItem() { Selected = EditItem.SIMCardStatusId == x.SIMCardStatusId, Text = x.SIMCardStatusName_en, Value = x.SIMCardStatusId.ToString() })
            };
            return View(model);
        }
        
        // POST: SIMCard/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public bool DeleteConfirmed(int[] ids)
        {
            //foreach (var id in ids)
            //{
            //    new SIMCardModel<SIMCard>().Delete(id);
            //}

            new SIMCardModel<SIMCard>().Delete(ids);
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Delete };
            return true;
        }

       
        [HttpPost]
        public ActionResult Import(FormCollection fc)
        {
            try
            {
                var file = Request.Files["file"];
                var result = new SIMCardModel<SIMCard>().Import(file);
               if(result)
                {
                    TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success , Transaction = Transactions.Import };
                    return RedirectToAction("Index");
                }

                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, Transaction = Transactions.Import };
                return RedirectToAction("Index");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is UnauthorizedAccessException)
                {
                    return RedirectToAction("Unauthorized", "Login");
                }

                throw ex;
            }

        }

        public ActionResult Export()
        {
            try
            {
                requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties= "SIMCardId,SerialNumber,GSM,SIMCardStatus.SIMCardStatusName_en", References = "SIMCardStatus" } };
                var fileBytes = new SIMCardModel<SIMCard>().Export(requestBody);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "SIMCards.xlsx");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is UnauthorizedAccessException)
                {
                    return RedirectToAction("Unauthorized", "Login");
                }
                throw ex;

            }


        }

        
    }
}
