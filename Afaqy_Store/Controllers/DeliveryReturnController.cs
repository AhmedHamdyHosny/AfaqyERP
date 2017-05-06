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
    public class DeliveryReturnController : BaseController<Transaction, DeliveryReturnViewModel, DeliveryReturnIndexViewModel, DeliveryReturnDetailsViewModel, DeliveryReturnCreateBindModel, DeliveryReturnEditBindModel, DeliveryReturnEditModel, Transaction, DeliveryReturnModel<Transaction>, DeliveryReturnModel<DeliveryReturnViewModel>>
    {
        public ActionResult Index(int? id = null)
        {
            ViewBag.DeliveryNoteId = id;
            return base.Index();
        }

        public ActionResult DeliveryNote(int id)
        {
            ViewBag.DeliveryNoteId = id;
            return base.Index();
        }
        

        public override void FuncPreDetailsView(object id, ref List<DeliveryReturnDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeliveryReturnModel<DeliveryReturnDetailsViewModel>().GetView<DeliveryReturnDetailsViewModel>(requestBody).PageItems;

            foreach (var item in items)
            {
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, Value = item.TransactionId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                requestBody = new GenericDataFormat() { Filters = filters };
                item.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;
            }
        }

        [ActionName("Create")]
        public ActionResult CreateDeliveryReturn(int id)
        {
            var model = new DeliveryReturnCreateBindModel();
            model.TransactionId = id;
            FuncPreInitCreateView(ref model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult CreateDeliveryReturn(DeliveryReturnCreateBindModel model)
        {
            if (ModelState.IsValid)
            {
                FuncPreCreate(ref model);
                //create instance to insert object
                var instance = new DeliveryReturnModel<Transaction>();
                var item = instance.Insert(model);

                return FuncPostCreate(ref model, ref item);
            }

            FuncPreInitCreateView(ref model);
            return View(model);
        }

        public void FuncPreInitCreateView(ref DeliveryReturnCreateBindModel model)
        {
            //bind delivery return info 
            model = new DeliveryReturnModel<DeliveryReturnCreateBindModel>().Get(model.TransactionId);

            //get delivery note info
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, Value = model.TransactionId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            var requestBody = new GenericDataFormat() { Filters = filters};
            model.DeliveryNote = new DeliveryNoteModel<DeliveryNoteViewModel>().GetView<DeliveryNoteViewModel>(requestBody).PageItems.SingleOrDefault();
            
            //get delivery details view info
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal,  Value = model.TransactionId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            requestBody = new GenericDataFormat() { Filters = filters };
            model.DeliveryNoteDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;
           
        }
        public override void FuncPreCreate(ref DeliveryReturnCreateBindModel model)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestTechnician" } };
            var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();
            
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            //for test (change status from new to return)
            model.TransactionStatusId = (int)Classes.Common.DBEnums.TransactionStatus.New;

            //get saleman id
            int? salesmanId = new CustomerModel<rpaux>().Get(model.Customer_aux_id).salecode;

            //set Dolphin Transaction
            model.DolphinTrans = new it_trans_a()
            {
                tra_sal_aux_id = salesmanId, //Saleman Id
                tra_cura_seq = 1, //Currency id (SAR)
                tra_status = "0", //initial status
                tra_user_id = User.DolphinUser, //dolphin user
                tra_sup_ref = model.TransactionReference, //Delivery Return Reference
                tra_ref_type = 12 //12 is delivery return type
            };
            
            var cmp_seq = model.cmp_seq;
            model.TransactionDetails = model.TransactionDetails.Select(x => { x.cmp_seq = cmp_seq; x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();
            foreach (var item in model.TransactionDetails)
            {
                item.TransactionItem = model.DeliveryDevice.Where(x => x.ModelType_ia_item_id != null && x.ModelType_ia_item_id == item.ModelType_ia_item_id)
                    .Select(x => new TransactionItem()
                    {
                        cmp_seq = cmp_seq,
                        TransactionDetailsId = x.TransactionDetailsId,
                        DeviceId = x.DeviceId,
                        Employee_aux_id = x.Employee_aux_id,
                        CreateUserId = User.UserId,
                        CreateDate = DateTime.Now
                    }).ToList();
            }

        }
        public override void FuncPreInitEditView(object id, ref Transaction EditItem, ref DeliveryReturnEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal,  Value = id, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "TransactionDetails,TransactionDetails.DeviceModelType" } };
                EditItem = new DeliveryReturnModel<Transaction>().Get(requestBody).SingleOrDefault();
            }
            if (EditItem != null)
            {
                model = new DeliveryReturnEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref DeliveryReturnEditBindModel EditItem)
        {
            id = EditItem.TransactionId;

            //get delivery return details 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "TransactionDetails" } };
            var DeliveryReturn = new DeliveryReturnModel<Transaction>().Get(requestBody).SingleOrDefault();
            var originalDeliveryDetails = DeliveryReturn.TransactionDetails;
            foreach (var item in EditItem.TransactionDetails)
            {
                var originalItem = originalDeliveryDetails.SingleOrDefault(x => x.ModelType_ia_item_id == item.ModelType_ia_item_id);
                if (originalItem != null)
                {
                    item.CreateUserId = originalItem.CreateUserId;
                    item.CreateDate = originalItem.CreateDate;
                    item.ModifyUserId = User.UserId;
                    item.ModifyDate = DateTime.Now;
                }
                else
                {
                    item.CreateUserId = User.UserId;
                    item.CreateDate = DateTime.Now;
                }
            }

            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeliveryReturn.xlsx";
            string properties = string.Join(",", typeof(Transaction).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar"))); ;
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

        //[ActionName("ServerAdd")]
        //public ActionResult ServerAdd(object id)
        //{
        //    filters = new List<GenericDataFormat.FilterItems>();
        //    filters.Add(new GenericDataFormat.FilterItems()
        //    {
        //        Property = "TransactionId",
        //        Operation = GenericDataFormat.FilterOperations.Equal,
        //        Value = id,
        //        LogicalOperation = GenericDataFormat.LogicalOperations.And
        //    });
        //    var requestBody = new GenericDataFormat() { Filters = filters };
        //    var items = new DeliveryReturnModel<DeliveryReturnDetailsViewModel>().GetView<DeliveryReturnDetailsViewModel>(requestBody).PageItems;
        //    DeliveryReturnDetailsViewModel Model = null;
        //    if (items != null && items.Count > 0)
        //    {
        //        Model = items.SingleOrDefault();

        //        //get delivery details
        //        filters = new List<GenericDataFormat.FilterItems>();
        //        filters.Add(new GenericDataFormat.FilterItems()
        //        {
        //            Property = "TransactionId",
        //            Operation = GenericDataFormat.FilterOperations.Equal,
        //            Value = id,
        //            LogicalOperation = GenericDataFormat.LogicalOperations.And
        //        });
        //        requestBody = new GenericDataFormat() { Filters = filters };
        //        Model.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;
                
        //        //get delivery devices
        //        filters = new List<GenericDataFormat.FilterItems>();
        //        filters.Add(new GenericDataFormat.FilterItems()
        //        {
        //            Property = "TransactionId",
        //            Operation = GenericDataFormat.FilterOperations.Equal,
        //            Value = id,
        //            LogicalOperation = GenericDataFormat.LogicalOperations.And
        //        });
        //        requestBody = new GenericDataFormat() { Filters = filters };
        //        Model.DeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
        //    }
        //    return View(Model);
        //}

        //[HttpPost]
        //public ActionResult ServerAdd(DeliveryDeviceEditBindModel[] DeliveryDevice, FormCollection fc)
        //{
        //    //get delivery device
        //    string DeliveryReturnId = fc["TransactionId"];

        //    //get delivery devices
        //    filters = new List<GenericDataFormat.FilterItems>();
        //    filters.Add(new GenericDataFormat.FilterItems()
        //    {
        //        Property = "TransactionId",
        //        Operation = GenericDataFormat.FilterOperations.Equal,
        //        Value = deliveryReturnId,
        //        LogicalOperation = GenericDataFormat.LogicalOperations.And
        //    });
        //    var requestBody = new GenericDataFormat() { Filters = filters };
        //    var oldDeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
        //    List<TransactionItem> UpdatedDeliveryDevice = new List<TransactionItem>();
        //    foreach (var oldDevice in oldDeliveryDevices)
        //    {
        //        TransactionItem deliveryDevice = new TransactionItem();

        //        //set old info
        //        Classes.Utilities.Utility.CopyObject<TransactionItem>(oldDevice, ref deliveryDevice);
                
        //        //set new info
        //        var newDeliveryDevice = DeliveryDevice.Where(x => x.TransactionItemId == oldDevice.TransactionItemId).SingleOrDefault();
        //        deliveryDevice.AddToServer = newDeliveryDevice.AddToServer;
        //        deliveryDevice.TrackWithTechnician = newDeliveryDevice.TrackWithTechnician;
        //        deliveryDevice.ServerUpdated = newDeliveryDevice.ServerUpdated;
        //        deliveryDevice.Note = newDeliveryDevice.Note;
        //        deliveryDevice.ModifyUserId = User.UserId;
        //        deliveryDevice.ModifyDate = DateTime.Now;

        //        UpdatedDeliveryDevice.Add(deliveryDevice);
        //    }
        //    //update delivery devices
        //    List<UpdateItemFormat<TransactionItem>> items = UpdatedDeliveryDevice.Select(x => new UpdateItemFormat<TransactionItem>() { id = x.TransactionItemId, newValue = x }).ToList();
        //    var updatedItems = new DeliveryItemModel<TransactionItem>().Update(items);

        //    //send notification to users
        //    string referenceURL = this.Url.Action("Details");
        //    if (DeliveryDeviceEditBindModel.SendAddToServerNotification(deliveryReturnId, referenceURL))
        //    {
        //        //do nothing
        //    }

        //    TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = updatedItems.Count, MessageContent = Resources.ServerManagement.DeliveryDeviceAddedToServerSuccessMessage };
        //    return RedirectToAction("Index");
        //}
        
        [ActionName("Partial_DeliveryNoteDetails")]
        public PartialViewResult Partial_DeliveryNoteDetails(string id = null, DeliveryNoteDetailsViewModel deliveryNote = null)
        {
            if (!string.IsNullOrEmpty(id) && deliveryNote == null)
            {
                //get delivery note details
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                var requestBody = new GenericDataFormat() { Filters = filters };
                var items = new DeliveryNoteModel<DeliveryNoteDetailsViewModel>().GetView<DeliveryNoteDetailsViewModel>(requestBody).PageItems;

                if (items != null && items.Count > 0)
                {
                    deliveryNote = items.SingleOrDefault();
                }
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = deliveryNote.TransactionId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                deliveryNote.DeliveryTechnicians = new DeliveryTechnicianModel<DeliveryTechnicianViewModel>().GetView<DeliveryTechnicianViewModel>(requestBody).PageItems;
            }

            return PartialView(deliveryNote);
        }
        
        #region Override Actions
        [NonAction]
        public override ActionResult Index()
        {
            return base.Index();
        }
        [NonAction]
        public override ActionResult Create()
        {
            return null;
        }
        [NonAction]
        public override ActionResult Create(DeliveryReturnCreateBindModel model)
        {
            return base.Create(model);
        }
        #endregion
    }
}
