using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static Classes.Common.Enums;

namespace Classes.Common
{
    public class GenericContoller<TDBModel,TViewModel, TCreateBindModel, TEditBindModel, TEditModel,TModel_TDBModel,TModel_TViewModel> : Controller 
    {
        public GenericDataFormat IndexRequestBody;
        public GenericDataFormat DetailsRequestBody;
        public GenericDataFormat ExportRequestBody;
        public List<Reference> CreateReferences = null;
        public List<Reference> EditReferences = null;
        public string ExportFileName = typeof(TDBModel).ToString() + ".xlsx";
        public List<ActionItemPropertyValue> ActionItemsPropertyValue = null;
        public List<PostActionExcuteRedirect> PostActionExcuteRedirects = null;
        public string PK_PropertyName { get; set; }
        
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
            var items = (List<TViewModel>)instance.Get(DetailsRequestBody);
            if (items.Count < 1 || items.ElementAt(0) == null)
            {
                return HttpNotFound();
            }
            dynamic model = (TViewModel)items.ElementAt(0);
            //model.BindCreate_Modify_User();
            return View(model);
        }

        [NonAction]
        public void InitCreateView()
        {
            if (CreateReferences != null && CreateReferences.Count > 0)
            {
                foreach (var reference in CreateReferences)
                {
                    //create instance of TModel of TViewModel from Reference TypeModel
                    dynamic instance = Activator.CreateInstance(reference.TypeModel);
                    var items = instance.Get();
                    ViewData[reference.ViewDataName] = new SelectList(items, reference.DataValueField, reference.DataTextField, reference.SelectedValue);
                }
            }
        }

        // GET: Controller/Create
        public virtual ActionResult Create()
        {
            InitCreateView();
            return View();
        }

        // POST: Controller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TCreateBindModel model)
        {
            if (ModelState.IsValid)
            {
                //set pre-insert property values
                if(ActionItemsPropertyValue != null)
                {
                    Type type = typeof(TCreateBindModel);
                    var createActionItemsPropertyValue = ActionItemsPropertyValue.Where(act => act.Transaction == Transactions.Create);
                    foreach (var actItmPropVal in createActionItemsPropertyValue)
                    {
                        PropertyInfo propertyInfo = model.GetType().GetProperties().Where(x => x.Name.Equals(actItmPropVal.PropertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(model, actItmPropVal.Value);
                        }
                    }
                }

                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var item = instance.Insert(model);

                //excute after insert action
                //var deviceStatusHistory = new DeviceStatusHistory();
                //deviceStatusHistory.DeviceId = device.DeviceId;
                //deviceStatusHistory.StatusId = device.DeviceStatusId;
                //deviceStatusHistory.CreateUserId = userId;
                //deviceStatusHistory.CreateDate = DateTime.Now;
                //new DeviceStatusHistoryModel<DeviceStatusHistory>().Insert(deviceStatusHistory);

                //set alerts messages
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };

                //go to after insert action
                if(PostActionExcuteRedirects != null)
                {
                    var postActionRedirect = PostActionExcuteRedirects.SingleOrDefault(x => x.Transaction == Transactions.Create);
                    if(postActionRedirect != null)
                    {
                        return RedirectToAction(postActionRedirect.RedirectToAction); 
                    }
                }
                return RedirectToAction("Index");
            }

            InitCreateView();
            return View(model);
        }

        [NonAction]
        public TEditModel InitEditView(TDBModel item)
        {
            //create instance of TEditModel 
            var model = Activator.CreateInstance(typeof(TEditModel));
            if (EditReferences != null && EditReferences.Count > 0)
            {
                foreach (var reference in EditReferences)
                {
                    //create instance of TModel of TViewModel from Reference TypeModel
                    dynamic ReferenceInstance = Activator.CreateInstance(reference.TypeModel);
                    //get list of reference items that can be linked with model item
                    IEnumerable<dynamic> refItems = ReferenceInstance.Get();
                    //get list of reference items as SelectListItem type
                    var refSelectListItems = refItems.Select(x => new SelectListItem()
                    {
                        Selected = (object)Utilities.Utility.GetPropertyValue(item, reference.DataValueField) == (object)Utilities.Utility.GetPropertyValue(x, reference.DataValueField),
                        Text = Utilities.Utility.GetPropertyValue(x, reference.DataTextField).ToString(),
                        Value = Utilities.Utility.GetPropertyValue(x, reference.DataValueField).ToString()
                    });
                    //set the value of reference SelectListItem property of EditModel object
                    PropertyInfo propertyInfo = model.GetType().GetProperties().Where(x => x.Name.Equals(reference.PropertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(model, refSelectListItems);
                    }
                }
            }
            //set the value of EditItem property EditModel object 
            Utilities.Utility.SetPropertyValue(ref model, "EditItem", item);
            return (TEditModel)model;
        }

        // GET: Controller/Edit/5
        public virtual ActionResult Edit(object id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get the item by id
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            var item = instance.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            var model = InitEditView(item);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TEditBindModel EditItem)
        {
            if (ModelState.IsValid)
            {
                //set pre-edit property values
                if (ActionItemsPropertyValue != null)
                {
                    Type type = typeof(TEditBindModel);
                    var editActionItemsPropertyValue = ActionItemsPropertyValue.Where(act => act.Transaction == Transactions.Edit);
                    foreach (var actItmPropVal in editActionItemsPropertyValue)
                    {
                        Utilities.Utility.SetPropertyValue<TEditBindModel>(ref EditItem, actItmPropVal.PropertyName, actItmPropVal.Value);
                    }
                }
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var id = Utilities.Utility.GetPropertyValue(EditItem, PK_PropertyName);
                var item = instance.Update(EditItem, id);
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
                //go to after edit action
                if (PostActionExcuteRedirects != null)
                {
                    var postActionRedirect = PostActionExcuteRedirects.SingleOrDefault(x => x.Transaction == Transactions.Create);
                    if (postActionRedirect != null)
                    {
                        return RedirectToAction(postActionRedirect.RedirectToAction);
                    }
                }
                return RedirectToAction("Index");
            }
            TDBModel editInstance = (TDBModel) Activator.CreateInstance(typeof(TDBModel));
            Utilities.Utility.CopyObject<TDBModel>( EditItem,ref editInstance);
            var model = InitEditView(editInstance);
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        public virtual bool DeleteConfirmed(object id)
        {
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Delete(id);
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Delete };
            return true;
        }

        [HttpPost, ActionName("DeleteGroup")]
        public virtual bool DeleteConfirmed(object[] ids)
        {
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Delete(ids);
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Delete };
            return true;
        }

        [HttpPost, ActionName("Hide")]
        public virtual bool HideConfirmed(object id)
        {
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Hide(id);
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Delete };
            return true;
        }

        [HttpPost, ActionName("HideGroup")]
        public virtual bool HideConfirmed(object[] ids)
        {
            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Hide(ids);
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
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ExportFileName);
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
        public string PropertyName { get; set; }
    }

    public class ActionItemPropertyValue
    {
        public Transactions Transaction { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }

    }

    public class PostActionExcuteRedirect
    {
        public Transactions Transaction{ get; set; }
        public string RedirectToAction { get; set; }
    }




}