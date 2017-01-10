using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Classes.Common.Enums;

namespace Classes.Common
{
    public class GenericContoller<TDBModel,TViewModel,TEditModel,TModel_TDBModel,TModel_TViewModel> : Controller 
    {
        public GenericDataFormat IndexRequestBody;
        public GenericDataFormat DetailsRequestBody;
        public GenericDataFormat ExportRequestBody;
        public List<Reference> References = null;
        public string ExcelFileName = typeof(TDBModel).ToString() + ".xlsx";

        // GET: Controller
        public virtual ActionResult Index()
        {
            if (TempData["AlertMessage"] != null)
            {
                ViewBag.AlertMessage = TempData["AlertMessage"];
            }
           
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TViewModel));
            var model = instance.Get(IndexRequestBody);
            
            return View(model);
        }

        // GET: Controller/Details/5
        public virtual ActionResult Details(object id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsRequestBody.Filters[0].Value = id;
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TViewModel));
            var items = instance.Get(DetailsRequestBody);
            if (items.Count < 1 || items.ElementAt(0) == null)
            {
                return HttpNotFound();
            }
            dynamic model = (TViewModel)items.ElementAt(0);
            model.BindCreate_Modify_User();
            return View(model);
        }

        // GET: Controller/Create
        public virtual ActionResult Create()
        {
            if(References != null && References.Count > 0)
            {
                foreach (var reference in References)
                {
                    //create instance of TModel of TViewModel from Reference TypeModel
                    dynamic instance = Activator.CreateInstance(reference.TypeModel);
                    var items = instance.Get();
                    ViewData[reference.ViewDataName] = new SelectList(items, reference.SelectedValue, reference.DataTextField,reference.SelectedValue);
                }
            }

            return View();
        }

        // POST: Controller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = CreateBind)] TDBModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //for test
        //        model.CreateUserId = 1;
        //        model.CreateDate = DateTime.Now;

        //        new SIMCardModel<SIMCard>().Insert(model);
        //        TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
        //        return RedirectToAction("Index");
        //    }

        //    var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();
        //    ViewBag.SIMCardStatusId = new SelectList(SIMCardStatus, "SIMCardStatusId", "SIMCardStatusName_en", model.SIMCardStatusId);
        //    return View(model);
        //}

        // GET: Controller/Edit/5
        //public ActionResult Edit(object id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //create TModel of TViewModel
        //    var TypeModel = typeof(TModel).MakeGenericType(typeof(TViewModel));
        //    //create instance of TModel of TViewModel
        //    dynamic instance = Activator.CreateInstance(TypeModel);
        //    var item = instance.Get(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
            
        //    //create instance of TEditModel 
        //    var model = Activator.CreateInstance(typeof(TEditModel));

        //    if (References != null && References.Count > 0)
        //    {
        //        foreach (var reference in References)
        //        {
        //            //create TModel of TViewModel from Reference TypeModel
        //            var ReferenceTypeModel = reference.TypeModel.MakeGenericType(reference.TypeViewModel);
        //            //create instance of TModel of TViewModel from Reference TypeModel
        //            dynamic ReferenceInstance = Activator.CreateInstance(ReferenceTypeModel);
        //            var items = ReferenceInstance.Get();
        //            ViewData[reference.ViewDataName] = new SelectList(items, reference.SelectedValue, reference.DataTextField, reference.SelectedValue);

        //            items.Select(x => new SelectListItem() { Selected = SimCard.SIMCardStatusId == x.SIMCardStatusId, Text = x.SIMCardStatusName_en, Value = x.SIMCardStatusId.ToString() })

        //            var model = new EditSIMCardModel()
        //            {
        //                EditItem = SimCard,
        //                Status =  
        //            };
        //        }
        //    }

        //    return View(model);
        //}

        // POST: Controller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SIMCardId,SerialNumber,GSM,SIMCardStatusId,CreateUserId,CreateDate")] SIMCard EditItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //for test
        //        EditItem.ModifyUserId = 1;
        //        EditItem.ModifyDate = DateTime.Now;

        //        new SIMCardModel<SIMCard>().Update(EditItem, EditItem.SIMCardId);
        //        TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
        //        return RedirectToAction("Index");
        //    }
        //    var SIMCardStatus = new SIMCardStatusModel<SIMCardStatus>().Get();
        //    var model = new EditSIMCardModel()
        //    {
        //        EditItem = new SIMCardModel<SIMCard>().Get(EditItem.SIMCardId),
        //        Status = SIMCardStatus.Select(x => new SelectListItem() { Selected = EditItem.SIMCardStatusId == x.SIMCardStatusId, Text = x.SIMCardStatusName_en, Value = x.SIMCardStatusId.ToString() })
        //    };
        //    return View(model);
        //}

        // POST: Controller/Delete/5

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public virtual bool DeleteConfirmed(object[] ids)
        {
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Delete(ids);
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Delete };
            return true;
        }

        // POST: Controller/Import
        [HttpPost]
        public virtual ActionResult Import(FormCollection fc)
        {
            try
            {
                var file = Request.Files["file"];
                //create instance of TModel of TViewModel
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var result = instance.Import(file);
                if (result)
                {
                    TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, Transaction = Transactions.Import };
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

        // POST: Controller/Export
        public virtual ActionResult Export()
        {
            try
            {
                //create instance of TModel of TDBModel
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var fileBytes = instance.Export(ExportRequestBody);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ExcelFileName);
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

    public class Reference
    {
        public Type TypeModel { get; set; }
        public string ViewDataName { get; set; }
        public string SelectColumns { get; set; }
        public string DataValueField { get; set; }
        public string DataTextField { get; set; }
        public string SelectedValue { get; set; }
    }
}