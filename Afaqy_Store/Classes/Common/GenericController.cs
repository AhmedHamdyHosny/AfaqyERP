﻿using Classes.Utilities;
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
    public partial class GenericContoller<TDBModel,TViewModel, TCreateBindModel, TEditBindModel, TEditModel,TModel_TDBModel,TModel_TViewModel> : Controller 
    {
        public List<GenericDataFormat.FilterItems> filters ;
        public string ExportFileName = typeof(TDBModel).ToString() + ".xlsx";
        //public string PK_PropertyName { get; set; }

        //public GenericDataFormat IndexRequestBody;
        //public GenericDataFormat DetailsRequestBody;
        //public GenericDataFormat ExportRequestBody;
        //public List<Reference> CreateReferences = null;
        //public List<Reference> EditReferences = null;
        //public List<ActionItemPropertyValue> ActionItemsPropertyValue = null;
        //public List<PostActionExcuteRedirect> PostActionExcuteRedirects = null;


        // GET: Controller
        public virtual ActionResult Index()
        {
            if (TempData["AlertMessage"] != null)
            {
                ViewBag.AlertMessage = TempData["AlertMessage"];
            }
            //create instance of List of TViewModel that hold data
            var model = (List<TViewModel>)Activator.CreateInstance(typeof(List<TViewModel>)); ; 
            DelegatePreIndexView delegatePreExecute = new DelegatePreIndexView(FuncPreIndexView);
            delegatePreExecute(ref model);
            DelegatePostIndexView delegatePostExecute = new DelegatePostIndexView(FuncPostIndexView);
            return delegatePostExecute(ref model);
        }
        // GET: Controller/Details/5
        public virtual ActionResult Details(object id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //create instance List of TViewModel
            var items = (List<TViewModel>)Activator.CreateInstance(typeof(List<TViewModel>));
            DelegatePreDetailsView delegatePreExecute = new DelegatePreDetailsView(FuncPreDetailsView);
            delegatePreExecute(id, ref items);
            if (items.Count < 1 || items.ElementAt(0) == null)
            {
                return HttpNotFound();
            }
            var model = (TViewModel)items.ElementAt(0);
            DelegatePostDetailsView delegatePostExecute = new DelegatePostDetailsView(FuncPostDetailsView);
            return delegatePostExecute(ref model);
        }
        //[NonAction]
        //public void InitCreateView()
        //{
        //    if (CreateReferences != null && CreateReferences.Count > 0)
        //    {
        //        foreach (var reference in CreateReferences)
        //        {
        //            //create instance of TModel of TViewModel from Reference TypeModel
        //            dynamic instance = Activator.CreateInstance(reference.TypeModel);
        //            var refSorts = new List<GenericDataFormat.SortItems>();
        //            refSorts.Add(new GenericDataFormat.SortItems() { Property = reference.DataTextField });
        //            GenericDataFormat requestBody = new GenericDataFormat() { Filters = reference.Filters, Includes = new GenericDataFormat.IncludeItems() { Properties = reference.SelectColumns },Sorts = refSorts };
        //            var items = requestBody != null ? instance.Get(requestBody) : instance.Get();
        //            var refSelectLst = new SelectList(items, reference.DataValueField, reference.DataTextField, reference.SelectedValue);
        //            ViewData[reference.ViewDataName] = refSelectLst.Select(x => new Helper.CustomSelectListItem(x));
        //        }
        //    }
        //}
        // GET: Controller/Create
        public virtual ActionResult Create()
        {
            DelegatePreInitCreateView delegatePreExecute = new DelegatePreInitCreateView(FuncPreInitCreateView);
            delegatePreExecute();

            DelegatePostInitCreateView delegatePostExecute = new DelegatePostInitCreateView(FuncPostInitCreateView);
            return delegatePostExecute();
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
                DelegatePreCreate  delegatePreExecute = new DelegatePreCreate(FuncPreCreate);
                delegatePreExecute(ref model);
                //create instance to insert object
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var item = instance.Insert(model);
                
                DelegatePostCreate delegatePostExecute = new DelegatePostCreate(FuncPostCreate);
                return delegatePostExecute(ref model);
            }

            DelegatePreInitCreateView delegatePreExecuteInitCreateView = new DelegatePreInitCreateView(FuncPreInitCreateView);
            delegatePreExecuteInitCreateView();
            return View(model);
        }
        // POST: Controller/CreateGroup
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CreateGroup")]
        public virtual ActionResult Create(TCreateBindModel[] items, FormCollection fc = null)
        {
            DelegatePreCreateGroup delegatePreExecute = new DelegatePreCreateGroup(FuncPreCreate);
            delegatePreExecute(items, fc);

            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Import(items);

            DelegatePostCreateGroup delegatePostExecute = new DelegatePostCreateGroup(FuncPostCreate);
            return delegatePostExecute(items, fc);
        }
        //[NonAction]
        //public TEditModel InitEditView(TDBModel item)
        //{
        //    //create instance of TEditModel 
        //    var model = Activator.CreateInstance(typeof(TEditModel));
        //    if (EditReferences != null && EditReferences.Count > 0)
        //    {
        //        foreach (var reference in EditReferences)
        //        {
        //            //create instance of TModel of TViewModel from Reference TypeModel
        //            dynamic ReferenceInstance = Activator.CreateInstance(reference.TypeModel);
        //            //get list of reference items that can be linked with model item
        //            IEnumerable<dynamic> refItems = ReferenceInstance.Get();
        //            //get list of reference items as SelectListItem type
        //            var refSelectListItems = refItems.Select(x => new Helper.CustomSelectListItem()
        //            {
        //                Selected = (int)Utilities.Utility.GetPropertyValue(item, reference.DataValueField) == (int)Utilities.Utility.GetPropertyValue(x, reference.DataValueField),
        //                Text = Utilities.Utility.GetPropertyValue(x, reference.DataTextField).ToString(),
        //                Value = Utilities.Utility.GetPropertyValue(x, reference.DataValueField).ToString()
        //            });
        //            //var refSelectListItems = refItems.Select(x => new Helper.CustomSelectListItem()
        //            //{
        //            //    Selected = (object)Utilities.Utility.GetPropertyValue(item, reference.DataValueField) == (object)Utilities.Utility.GetPropertyValue(x, reference.DataValueField),
        //            //    Text = Utilities.Utility.GetPropertyValue(x, reference.DataTextField).ToString(),
        //            //    Value = Utilities.Utility.GetPropertyValue(x, reference.DataValueField).ToString()
        //            //});
        //            //set the value of reference SelectListItem property of EditModel object
        //            PropertyInfo propertyInfo = model.GetType().GetProperties().Where(x => x.Name.Equals(reference.PropertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        //            if (propertyInfo != null)
        //            {
        //                propertyInfo.SetValue(model, refSelectListItems);
        //            }
        //        }
        //    }
        //    //set the value of EditItem property EditModel object 
        //    Utilities.Utility.SetPropertyValue(ref model, "EditItem", item);
        //    return (TEditModel)model;
        //}
        //GET: Controller/Edit/5
        public virtual ActionResult Edit(object id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //create instance of TDBModel
            var EditItem = (TDBModel)Activator.CreateInstance(typeof(TDBModel));
            //create instance of TEditModel 
            var model = (TEditModel)Activator.CreateInstance(typeof(TEditModel));

            DelegatePreInitEditView delegatePreExecute = new DelegatePreInitEditView(FuncPreInitEditView);
            delegatePreExecute(id, ref EditItem, ref model);

            if (EditItem == null)
            {
                return HttpNotFound();
            }

            DelegatePostInitEditView delegatePostExecute = new DelegatePostInitEditView(FuncPostInitEditView);
            return delegatePostExecute(id, ref model);
            //return View(model);
        }
        // Post: Controller/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TEditBindModel EditItem)
        {
            object id = null;
            if (ModelState.IsValid)
            {
                //set pre-edit property values
                //if (ActionItemsPropertyValue != null)
                //{
                //    Type type = typeof(TEditBindModel);
                //    var editActionItemsPropertyValue = ActionItemsPropertyValue.Where(act => act.Transaction == Transactions.Edit);
                //    foreach (var actItmPropVal in editActionItemsPropertyValue)
                //    {
                //        Utilities.Utility.SetPropertyValue<TEditBindModel>(ref EditItem, actItmPropVal.PropertyName, actItmPropVal.Value);
                //    }
                //}
                //dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                //var id = Utilities.Utility.GetPropertyValue(EditItem, PK_PropertyName);
                //var item = instance.Update(EditItem, id);
                //TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
                ////go to after edit action
                //if (PostActionExcuteRedirects != null)
                //{
                //    var postActionRedirect = PostActionExcuteRedirects.SingleOrDefault(x => x.Transaction == Transactions.Create);
                //    if (postActionRedirect != null)
                //    {
                //        return RedirectToAction(postActionRedirect.RedirectToAction);
                //    }
                //}
                //return RedirectToAction("Index");
                
                DelegatePreEdit delegatePreExecute = new DelegatePreEdit(FuncPreEdit);
                delegatePreExecute(ref id, ref EditItem);
                
                //create instance to update object
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var item = instance.Update(EditItem, id);

                DelegatePostEdit delegatePostExecute = new DelegatePostEdit(FuncPostEdit);
                return delegatePostExecute(ref EditItem);


            }
            //TDBModel editInstance = (TDBModel) Activator.CreateInstance(typeof(TDBModel));
            //Utilities.Utility.CopyObject<TDBModel>( EditItem,ref editInstance);
            //var model = InitEditView(editInstance);
            //return View(model);

            TDBModel editInstance = (TDBModel)Activator.CreateInstance(typeof(TDBModel));
            Utilities.Utility.CopyObject<TDBModel>(EditItem, ref editInstance);
            //create instance of TEditModel 
            var model = (TEditModel)Activator.CreateInstance(typeof(TEditModel));
            DelegatePreInitEditView delegatePreExecuteInitEditView = new DelegatePreInitEditView(FuncPreInitEditView);
            delegatePreExecuteInitEditView(id,ref editInstance, ref model);
            return View(model);
        }
        // POST: Controller/Delete
        [HttpPost, ActionName("Delete")]
        public virtual bool DeleteConfirmed(object id)
        {
            DelegatePreDelete delegatePreExecute = new DelegatePreDelete(FuncPreDelete);
            delegatePreExecute(id);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Delete(id);

            DelegatePostDelete delegatePostExecute = new DelegatePostDelete(FuncPostDelete);
            return delegatePostExecute(id);
            
        }
        // POST: Controller/Delete
        [HttpPost, ActionName("DeleteGroup")]
        public virtual bool DeleteConfirmed(object[] ids)
        {
            DelegatePreDeleteGroup delegatePreExecute = new DelegatePreDeleteGroup(FuncPreDelete);
            delegatePreExecute(ids);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Delete(ids);

            DelegatePostDeleteGroup delegatePostExecute = new DelegatePostDeleteGroup(FuncPostDelete);
            return delegatePostExecute(ids);
        }
        // POST: Controller/Deactive
        [HttpPost, ActionName("Deactive")]
        public virtual bool DeactiveConfirmed(object id)
        {
            DelegatePreDeactivate delegatePreExecute = new DelegatePreDeactivate(FuncPreDeactivate);
            delegatePreExecute(id);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Deactive(id);

            DelegatePostDeactivate delegatePostExecute = new DelegatePostDeactivate(FuncPostDeactivate);
            return delegatePostExecute(id);
        }
        // POST: Controller/Deactive
        [HttpPost, ActionName("DeactiveGroup")]
        public virtual bool DeactiveGroupConfirmed(object[] ids)
        {
            DelegatePreDeactivateGroup delegatePreExecute = new DelegatePreDeactivateGroup(FuncPreDeactivate);
            delegatePreExecute(ids);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Deactive(ids);

            DelegatePostDeactivateGroup delegatePostExecute = new DelegatePostDeactivateGroup(FuncPostDeactivate);
            return delegatePostExecute(ids);
        }
        // POST: Controller/Active
        [HttpPost, ActionName("Active")]
        public virtual bool ActiveConfirmed(object id)
        {
            DelegatePreActivate delegatePreExecute = new DelegatePreActivate(FuncPreActivate);
            delegatePreExecute(id);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Active(id);

            DelegatePostActivate delegatePostExecute = new DelegatePostActivate(FuncPostActivate);
            return delegatePostExecute(id);
        }
        // POST: Controller/active
        [HttpPost, ActionName("ActiveGroup")]
        public virtual bool ActiveGroupConfirmed(object[] ids)
        {
            DelegatePreActivateGroup delegatePreExecute = new DelegatePreActivateGroup(FuncPreActivate);
            delegatePreExecute(ids);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Active(ids);

            DelegatePostActivateGroup delegatePostExecute = new DelegatePostActivateGroup(FuncPostActivate);
            return delegatePostExecute(ids);
        }
        // POST: Controller/Import
        [HttpPost]
        public virtual ActionResult Import(FormCollection fc)
        {
            try
            {
                DelegatePreImport delegatePreExecute = new DelegatePreImport(FuncPreImport);
                delegatePreExecute(fc);

                var file = Request.Files["file"];
                //create instance of TModel of TViewModel
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var result = instance.Import(file);

                DelegatePostImport delegatePostExecute = new DelegatePostImport(FuncPostImport);
                return delegatePostExecute(result);
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
                var ExportRequestBody = new GenericDataFormat();
                DelegatePreExport delegatePreExecute = new DelegatePreExport(FuncPreExport);
                delegatePreExecute(ref ExportRequestBody, ref this.ExportFileName);

                //create instance of TModel of TDBModel
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var fileBytes = instance.Export(ExportRequestBody);

                DelegatePostExport delegatePostExecute = new DelegatePostExport(FuncPostExport);
                return delegatePostExecute(fileBytes, this.ExportFileName);
                
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
    //public class Reference
    //{
    //    public Type TypeModel { get; set; }
    //    public string ViewDataName { get; set; }
    //    public string SelectColumns { get; set; }
    //    public string DataValueField { get; set; }
    //    public string DataTextField { get; set; }
    //    public string SelectedValue { get; set; }
    //    public string PropertyName { get; set; }
    //    public List<GenericDataFormat.FilterItems> Filters { get; set; }
    //}
    //public class ActionItemPropertyValue
    //{
    //    public Transactions Transaction { get; set; }
    //    public string PropertyName { get; set; }
    //    public object Value { get; set; }

    //}
    //public class PostActionExcuteRedirect
    //{
    //    public Transactions Transaction{ get; set; }
    //    public string RedirectToAction { get; set; }
    //}

    public partial class GenericContoller<TDBModel, TViewModel, TCreateBindModel, TEditBindModel, TEditModel, TModel_TDBModel, TModel_TViewModel>
    {
        #region Delegates
        public delegate void DelegatePreIndexView(ref List<TViewModel> model);
        public delegate ActionResult DelegatePostIndexView(ref List<TViewModel> model);
        public delegate void DelegatePreDetailsView(object id, ref List<TViewModel> items);
        public delegate ActionResult DelegatePostDetailsView(ref TViewModel model);
        public delegate void DelegatePreInitCreateView();
        public delegate ActionResult DelegatePostInitCreateView();
        public delegate void DelegatePreCreate(ref TCreateBindModel model);
        public delegate ActionResult DelegatePostCreate(ref TCreateBindModel model);
        public delegate void DelegatePreCreateGroup(TCreateBindModel[] items, FormCollection formCollection);
        public delegate ActionResult DelegatePostCreateGroup(TCreateBindModel[] items, FormCollection formCollection);
        public delegate void DelegatePreInitEditView(object id, ref TDBModel EditItem, ref TEditModel model);
        public delegate ActionResult DelegatePostInitEditView(object id, ref TEditModel model);
        public delegate void DelegatePreEdit(ref object id,ref TEditBindModel EditItem);
        public delegate ActionResult DelegatePostEdit(ref TEditBindModel EditItem);
        public delegate void DelegatePreDelete(object id);
        public delegate bool DelegatePostDelete(object id);
        public delegate void DelegatePreDeleteGroup(object[] ids);
        public delegate bool DelegatePostDeleteGroup(object[] ids);
        public delegate void DelegatePreDeactivate(object id);
        public delegate bool DelegatePostDeactivate(object id);
        public delegate void DelegatePreDeactivateGroup(object[] ids);
        public delegate bool DelegatePostDeactivateGroup(object[] ids);
        public delegate void DelegatePreActivate(object id);
        public delegate bool DelegatePostActivate(object id);
        public delegate void DelegatePreActivateGroup(object[] ids);
        public delegate bool DelegatePostActivateGroup(object[] ids);
        public delegate void DelegatePreImport(FormCollection formCollection);
        public delegate ActionResult DelegatePostImport(bool result);
        public delegate void DelegatePreExport(ref GenericDataFormat ExportRequestBody,ref string ExportFileName);
        public delegate ActionResult DelegatePostExport(byte[] fileBytes, string ExportFileName);
        #endregion

        #region Delegate Functions
        public virtual void FuncPreIndexView(ref List<TViewModel> model)
        {

        }
        public virtual ActionResult FuncPostIndexView(ref List<TViewModel> model)
        {
            return View(model);
        }
        public virtual void FuncPreDetailsView(object id, ref List<TViewModel> items)
        {

        }
        public virtual ActionResult FuncPostDetailsView(ref TViewModel model)
        {
            return View(model);
        }
        public virtual void FuncPreInitCreateView()
        {

        }
        public virtual ActionResult FuncPostInitCreateView()
        {
            return View();
        }
        public virtual void FuncPreCreate(ref TCreateBindModel model)
        {

        }
        public virtual ActionResult FuncPostCreate(ref TCreateBindModel model)
        {
            //set alerts messages
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
            //go to after insert action
            return RedirectToAction("Index");
        }
        public virtual void FuncPreCreate(TCreateBindModel[] items, FormCollection formCollection)
        {
        }
        public virtual ActionResult FuncPostCreate(TCreateBindModel[] items, FormCollection formCollection)
        {
            //set alerts messages
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = items.Length, Transaction = Transactions.Create };
            return RedirectToAction("Index");
        }
        public virtual void FuncPreInitEditView(object id, ref TDBModel EditItem, ref TEditModel model)
        {

        }
        public virtual ActionResult FuncPostInitEditView(object id, ref TEditModel model)
        {
            return View(model);
        }
        public virtual void FuncPreEdit(ref object id, ref TEditBindModel EditItem)
        {

        }
        public virtual ActionResult FuncPostEdit(ref TEditBindModel EditItem)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
            return RedirectToAction("Index");
        }
        public virtual void FuncPreDelete(object id)
        {

        }
        public virtual bool FuncPostDelete(object id)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Delete };
            return true;
        }
        public virtual void FuncPreDelete(object[] ids)
        {

        }
        public virtual bool FuncPostDelete(object[] ids)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Delete };
            return true;
        }
        public virtual void FuncPreDeactivate(object id)
        {

        }
        public virtual bool FuncPostDeactivate(object id)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Deactive };
            return true;
        }
        public virtual void FuncPreDeactivate(object[] ids)
        {

        }
        public virtual bool FuncPostDeactivate(object[] ids)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Deactive };
            return true;
        }
        public virtual void FuncPreActivate(object id)
        {

        }
        public virtual bool FuncPostActivate(object id)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Active };
            return true;
        }
        public virtual void FuncPreActivate(object[] ids)
        {

        }
        public virtual bool FuncPostActivate(object[] ids)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Active };
            return true;
        }
        public virtual void FuncPreImport(FormCollection formCollection)
        {

        }
        public virtual ActionResult FuncPostImport(bool result)
        {
            if (result)
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, Transaction = Transactions.Import };
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, Transaction = Transactions.Import };
            return RedirectToAction("Index");
        }
        public virtual void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {

        }
        public virtual ActionResult FuncPostExport(byte[] fileBytes,string ExportFileName)
        {
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ExportFileName);
        }
        #endregion
        

    }




}