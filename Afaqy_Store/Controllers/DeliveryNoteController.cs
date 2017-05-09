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
    public class DeliveryNoteController : BaseController<Transaction,DeliveryNoteViewModel,DeliveryNoteIndexViewModel, DeliveryNoteDetailsViewModel, DeliveryNoteCreateBindModel, DeliveryNoteEditBindModel, DeliveryNoteEditModel, Transaction, DeliveryNoteModel<Transaction>, DeliveryNoteModel<DeliveryNoteViewModel>>
    {
        public ActionResult Index(int? id = null)
        {
            ViewBag.DeliveryRequestId = id;
            return base.Index();
        }

        public ActionResult DeliveryRequest(int id)
        {
            ViewBag.DeliveryRequestId = id;
            return base.Index();
        }

        public ActionResult Report(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public override void FuncPreDetailsView(object id, ref List<DeliveryNoteDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeliveryNoteModel<DeliveryNoteDetailsViewModel>().GetView<DeliveryNoteDetailsViewModel>(requestBody).PageItems;

            foreach (var item in items)
            {
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, Value = item.TransactionId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                requestBody = new GenericDataFormat() { Filters = filters };
                item.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;
            }

        }

        [ActionName("Create")]
        public ActionResult CreateDeliveryNote(int id)
        {
            //make sure that delivery note is Added to server
            //get delivery note if exist
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var deliveryNote = new DeliveryNoteModel<Transaction>().Get(requestBody);
            if(deliveryNote == null || deliveryNote.Count == 0)
            {
                var model = new DeliveryNoteCreateBindModel();
                model.DeliveryRequestId = id;
                FuncPreInitCreateView(ref model);
                return View(model);
            }
            else
            {
                return Details(id);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult CreateDeliveryNote(DeliveryNoteCreateBindModel model)
        {
            if (ModelState.IsValid)
            {
                FuncPreCreate(ref model);
                //create instance to insert object
                var instance = new DeliveryNoteModel<Transaction>();
                var item = instance.Insert(model);
                if(item.TransactionId != 0)
                {
                    return FuncPostCreate(ref model, ref item);
                }
                else
                {
                    //set alerts messages
                    TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Error, TransactionCount = 1, Transaction = Classes.Common.Enums.Transactions.Create };
                    //go to after insert action
                    return RedirectToAction("Index");
                }
            }
            
            FuncPreInitCreateView(ref model);
            return View(model);
        }

        public override ActionResult FuncPostCreate(ref DeliveryNoteCreateBindModel model, ref Transaction insertedItem)
        {
            NotificationCreateBindModel notification = new NotificationCreateBindModel();
            string referenceURL = this.Url.Action("Details");
            string serverAddURL = this.Url.Action("ServerAdd");
            if (!notification.SendInDeliveryPhaseNotification(insertedItem.TransactionId.ToString(),User.UserId, referenceURL, serverAddURL))
            {
                //do nothing
            }
            return base.FuncPostCreate(ref model, ref insertedItem);
        }
        public void FuncPreInitCreateView(ref DeliveryNoteCreateBindModel model)
        {
            DeliveryNoteDetailsViewModel deliveryNote = new DeliveryNoteDetailsViewModel();
            
            //get delivery request view info
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            var requestBody = new GenericDataFormat() { Filters = filters};
            var deliveryRequestView = new DeliveryRequestModel<DeliveryRequestViewModel>().GetView<DeliveryRequestViewModel>(requestBody).PageItems.SingleOrDefault();

            //get delivery request details view info
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = model.DeliveryRequestId });
            requestBody = new GenericDataFormat() { Filters = filters };
            var deliveryRequestDetails = new DeliveryRequestDetailsModel<DeliveryRequestDetailsView>().GetView<DeliveryRequestDetailsView>(requestBody).PageItems;

            model.DeliveryRequestDetails = deliveryRequestDetails;
            //map delivery request to delivery note
            deliveryNote.TransactionTypeId = (int)Classes.Common.DBEnums.TransactionType.Delivery_Note;
            deliveryNote.DeliveryRequestId = deliveryRequestView.DeliveryRequestId;
            deliveryNote.cmp_seq = deliveryRequestView.cmp_seq;
            deliveryNote.POS_ps_code = deliveryRequestView.POS_ps_code;
            deliveryNote.ps_name = deliveryRequestView.ps_name;
            deliveryNote.ps_altname = deliveryRequestView.ps_altname;
            deliveryNote.ps_shname = deliveryRequestView.ps_shname;
            deliveryNote.ps_shaltname = deliveryRequestView.ps_shaltname;
            deliveryNote.Warehouse_wa_code = deliveryRequestView.Warehouse_wa_code;
            deliveryNote.wa_name = deliveryRequestView.wa_name;
            deliveryNote.wa_altname = deliveryRequestView.wa_altname;
            deliveryNote.wa_shname = deliveryRequestView.wa_shname;
            deliveryNote.wa_shaltname = deliveryRequestView.wa_shaltname;
            deliveryNote.Customer_aux_id = deliveryRequestView.Customer_aux_id;
            deliveryNote.CustomerName = deliveryRequestView.CustomerName;
            //get dolphin customer name
            var dolphinCustomer = new CustomerModel<rpaux>().Get(deliveryRequestView.Customer_aux_id);
            deliveryNote.DolphinCustomerName = dolphinCustomer != null ? dolphinCustomer.name : null;
            //get customer account name
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerId", Operation = GenericDataFormat.FilterOperations.Equal, Value = deliveryRequestView.Customer_aux_id, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            //filters.Add(new GenericDataFormat.FilterItems() { Property = "SystemServerId", Operation = GenericDataFormat.FilterOperations.Equal, Value = deliveryRequestView.sys, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            requestBody = new GenericDataFormat() { Filters = filters };
            var customerAccount = new CustomerServerAccountModel<CustomerServerAccount>().Get(requestBody);
            deliveryNote.CustomerAccountName = customerAccount != null && customerAccount.FirstOrDefault() != null ? customerAccount.FirstOrDefault().SeverCustomerName : null;
            deliveryNote.CustomerContact_serial = deliveryRequestView.CustomerContact_serial;
            deliveryNote.contact_name = deliveryRequestView.contact_name;
            deliveryNote.AlternativeContactName = deliveryRequestView.AlternativeContactName;
            deliveryNote.AlternativeContactTelephone = deliveryRequestView.AlternativeContactTelephone;
            deliveryNote.SaleTransactionTypeId = deliveryRequestView.SaleTransactionTypeId;
            deliveryNote.SaleTransactionType_ar = deliveryRequestView.SaleTransactionType_ar;
            deliveryNote.SaleTransactionType_en = deliveryRequestView.SaleTransactionType_en;
            deliveryNote.TransactionDateTime = DateTime.Parse(deliveryRequestView.ActualDeliveryDateTime);
            //get delivery reference
            deliveryNote.TransactionReference = model.GetNewDeliveryReference(deliveryRequestView.Warehouse_wa_code);
            deliveryNote.SystemId = deliveryRequestView.SystemId;
            deliveryNote.SystemName = deliveryRequestView.SystemName;
            deliveryNote.WithInstallationService = deliveryRequestView.WithInstallationService;
            //get delivery request technician view
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = deliveryRequestView.DeliveryRequestId });
            requestBody = new GenericDataFormat() { Filters = filters };
            var deliveryRequestTechnician = new DeliveryRequestTechnicianModel<DeliveryRequestTechnicianViewModel>().GetView<DeliveryRequestTechnicianViewModel>(requestBody).PageItems;
            deliveryNote.DeliveryTechnicians = deliveryRequestTechnician.Select(x => new DeliveryTechnicianViewModel() { Employee_aux_id = x.Employee_aux_id, name = x.name, altname = x.altname }).ToList();
            ViewBag.DeliveryNote = deliveryNote;

            //get all Item Families
            filters = null;
            List<im_family> itemFamilies = new ItemFamilyModel<im_family>().GetAsDDLst("fa_cmp_seq,fa_code,fa_name,fa_altname", "fa_code", filters);
            ViewBag.ItemFamilies = itemFamilies.Select(x => new SelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.fa_name, x.fa_altname), Value = x.fa_code }).ToList();
            //get all model types
            filters = null;
            var sorts = new List<GenericDataFormat.SortItems>();
            sorts.Add(new GenericDataFormat.SortItems() { Property = "ia_item_code" });
            requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "ia_item_id,ia_item_code,ia_name,ia_altname,ia_cmp_seq,ia_fa_code" }, Sorts = sorts };
            List<im_itema> modelTypes = new ModelTypeModel<im_itema>().Get(requestBody);
            ViewBag.ModelTypes = modelTypes.Select(x => new SelectListItem() { Text = x.ia_item_code, Value = x.ia_item_id.ToString(), Group = new SelectListGroup() { Name = x.ia_fa_code } }).ToList();
            
        }
        public override void FuncPreCreate(ref DeliveryNoteCreateBindModel model)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestTechnician" } };
            var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();

            model.ToDeliveryNote(deliveryRequest);
            model.TransactionTechnician = deliveryRequest.DeliveryRequestTechnician.Select(x => new TransactionTechnician() { cmp_seq = x.cmp_seq, Employee_aux_id = x.Employee_aux_id, CreateUserId = User.UserId, CreateDate = DateTime.Now }).ToList();

            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
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
                tra_sup_ref = model.TransactionReference, //Delivery Note Reference
                tra_ref_type = 5 //5 is delivery note type
            };


            var cmp_seq = model.cmp_seq;
            model.TransactionDetails = model.TransactionDetails.Select(x => { x.cmp_seq = cmp_seq; x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();
            foreach (var item in model.TransactionDetails)
            {
                item.TransactionItem = model.DeliveryDevice.Where(x => x.ModelType_ia_item_id != null && x.ModelType_ia_item_id  == item.ModelType_ia_item_id)
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
        public override void FuncPreInitEditView(object id, ref Transaction EditItem, ref DeliveryNoteEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
                var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryDetails,DeliveryDetails.DeviceModelType" } };
                EditItem = new DeliveryNoteModel<Transaction>().Get(requestBody).SingleOrDefault();
            }
            if (EditItem != null)
            {
                model = new DeliveryNoteEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref DeliveryNoteEditBindModel EditItem)
        {
            id = EditItem.TransactionId;

            //get delivery request details 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "TransactionId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails" } };
            var deliveryNote = new DeliveryNoteModel<Transaction>().Get(requestBody).SingleOrDefault();
            var originalDeliveryDetails = deliveryNote.TransactionDetails;
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
            ExportFileName = "DeliveryNote.xlsx";
            string properties = string.Join(",", typeof(Transaction).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar"))); ;
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

        [ActionName("ServerAdd")]
        public ActionResult ServerAdd(object id)
        {
            //make sure that delivery note is in New Status
            var deliveryNote = new DeliveryNoteModel<Transaction>().Get(id);
            if(deliveryNote.TransactionStatusId == (int)Classes.Common.DBEnums.TransactionStatus.New)
            {
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
                DeliveryNoteDetailsViewModel Model = null;
                if (items != null && items.Count > 0)
                {
                    Model = items.SingleOrDefault();

                    //get delivery details
                    filters = new List<GenericDataFormat.FilterItems>();
                    filters.Add(new GenericDataFormat.FilterItems()
                    {
                        Property = "TransactionId",
                        Operation = GenericDataFormat.FilterOperations.Equal,
                        Value = id,
                        LogicalOperation = GenericDataFormat.LogicalOperations.And
                    });
                    requestBody = new GenericDataFormat() { Filters = filters };
                    Model.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;

                    //get delivery technicians
                    filters = new List<GenericDataFormat.FilterItems>();
                    filters.Add(new GenericDataFormat.FilterItems()
                    {
                        Property = "TransactionId",
                        Operation = GenericDataFormat.FilterOperations.Equal,
                        Value = id,
                        LogicalOperation = GenericDataFormat.LogicalOperations.And
                    });
                    requestBody = new GenericDataFormat() { Filters = filters };
                    Model.DeliveryTechnicians = new DeliveryTechnicianModel<DeliveryTechnicianViewModel>().GetView<DeliveryTechnicianViewModel>(requestBody).PageItems;

                    //get delivery devices
                    filters = new List<GenericDataFormat.FilterItems>();
                    filters.Add(new GenericDataFormat.FilterItems()
                    {
                        Property = "TransactionId",
                        Operation = GenericDataFormat.FilterOperations.Equal,
                        Value = id,
                        LogicalOperation = GenericDataFormat.LogicalOperations.And
                    });
                    requestBody = new GenericDataFormat() { Filters = filters };
                    Model.DeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
                }
                return View(Model);
            }
            else
            {
                return Details(id);
            }
            
        }

        [HttpPost]
        public ActionResult ServerAdd(DeliveryDeviceEditBindModel[] DeliveryDevice, FormCollection fc)
        {
            //get delivery device
            string deliveryNoteId = fc["TransactionId"];

            //get delivery devices
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "TransactionId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryNoteId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var oldDeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
            List<TransactionItem> UpdatedDeliveryDevice = new List<TransactionItem>();
            foreach (var oldDevice in oldDeliveryDevices)
            {
                TransactionItem deliveryDevice = new TransactionItem();

                //set old info
                Classes.Utilities.Utility.CopyObject<TransactionItem>(oldDevice, ref deliveryDevice);
                //deliveryDevice.DeliveryItemId = oldDevice.DeliveryItemId;
                //deliveryDevice.cmp_seq = oldDevice.DeliveryDevice_cmp_seq;
                //deliveryDevice.DeliveryDetailsId = oldDevice.DeliveryDetailsId;
                //deliveryDevice.DeviceId = oldDevice.DeviceId;
                //deliveryDevice.Employee_aux_id = oldDevice.Employee_aux_id;
                //deliveryDevice.InstallingDateTime = oldDevice.InstallingDateTime;
                //deliveryDevice.DeviceNaming_en = oldDevice.DeviceNaming_en;
                //deliveryDevice.DeviceNaming_ar = oldDevice.DeviceNaming_ar;
                //deliveryDevice.DeviceNamingTypeId = oldDevice.DeviceNamingTypeId;
                //deliveryDevice.TechnicalApproval = oldDevice.TechnicalApproval;
                //deliveryDevice.DeliveryReturn = oldDevice.DeliveryReturn;
                //deliveryDevice.IsBlock = oldDevice.IsBlock;
                //deliveryDevice.CreateUserId = oldDevice.CreateUserId;
                //deliveryDevice.CreateDate = oldDevice.CreateDate;

                //set new info
                var newDeliveryDevice = DeliveryDevice.Where(x => x.TransactionItemId == oldDevice.TransactionItemId).SingleOrDefault();
                deliveryDevice.AddToServer = newDeliveryDevice.AddToServer;
                deliveryDevice.TrackWithTechnician = newDeliveryDevice.TrackWithTechnician;
                deliveryDevice.ServerUpdated = newDeliveryDevice.ServerUpdated;
                deliveryDevice.Note = newDeliveryDevice.Note;
                deliveryDevice.ModifyUserId = User.UserId;
                deliveryDevice.ModifyDate = DateTime.Now;

                UpdatedDeliveryDevice.Add(deliveryDevice);
            }
            //update delivery devices
            List<UpdateItemFormat<TransactionItem>> items = UpdatedDeliveryDevice.Select(x => new UpdateItemFormat<TransactionItem>() { id = x.TransactionItemId, newValue = x }).ToList();
            var updatedItems = new DeliveryItemModel<TransactionItem>().Update(items);

            NotificationCreateBindModel notification = new NotificationCreateBindModel();
            //update Sever Employee Notification for this delivery
            if(!notification.UpdateDeliveryNoteServerAdd(deliveryNoteId))
            {
                //do nothing
            }

            //send notification to users
            string referenceURL = this.Url.Action("Details");
            if (!notification.SendServerAddedNotification(deliveryNoteId, referenceURL))
            {
                //do nothing
            }
            
            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = updatedItems.Count, MessageContent = Resources.ServerManagement.DeliveryDeviceAddedToServerSuccessMessage };
            return RedirectToAction("Index");
        }

        [ActionName("TechnicalApproval")]
        public ActionResult TechnicalApproval(object id)
        {
            //make sure that delivery note is Added to server
            //for test

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
            DeliveryNoteDetailsViewModel Model = null;
            if (items != null && items.Count > 0)
            {
                Model = items.SingleOrDefault();

                //get delivery details
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;

                //get delivery technicians
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryTechnicians = new DeliveryTechnicianModel<DeliveryTechnicianViewModel>().GetView<DeliveryTechnicianViewModel>(requestBody).PageItems;

                //get delivery devices
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult TechnicalApproval(DeliveryDeviceEditBindModel[] DeliveryDevice, FormCollection fc)
        {
            //get delivery device
            string deliveryNoteId = fc["TransactionId"];

            //get delivery devices
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "TransactionId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryNoteId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var oldDeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
            List<TransactionItem> UpdatedDeliveryDevice = new List<TransactionItem>();
            foreach (var oldDevice in oldDeliveryDevices)
            {
                TransactionItem deliveryDevice = new TransactionItem();
                
                //set old info
                Classes.Utilities.Utility.CopyObject<TransactionItem>(oldDevice, ref deliveryDevice);

                //set new info
                var newDeliveryDevice = DeliveryDevice.Where(x => x.TransactionItemId == oldDevice.TransactionItemId).SingleOrDefault();
                deliveryDevice.TechnicalApproval = newDeliveryDevice.TechnicalApproval;
                deliveryDevice.Note = newDeliveryDevice.Note;
                deliveryDevice.ModifyUserId = User.UserId;
                deliveryDevice.ModifyDate = DateTime.Now;

                UpdatedDeliveryDevice.Add(deliveryDevice);
            }
            //update delivery devices
            List<UpdateItemFormat<TransactionItem>> items = UpdatedDeliveryDevice.Select(x => new UpdateItemFormat<TransactionItem>() { id = x.TransactionItemId, newValue = x }).ToList();
            var updatedItems = new DeliveryItemModel<TransactionItem>().Update(items);

            //send notification to users
            string referenceURL = this.Url.Action("Details");
            NotificationCreateBindModel notification = new NotificationCreateBindModel();
            if (!notification.SendDeliveryNoteTechnicalApprovedNotification(deliveryNoteId, referenceURL, Url.Action("DeviceNaming")))
            {
                //do nothing
            }

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = updatedItems.Count, MessageContent = Resources.ServerManagement.DeliveryDeviceAddedToServerSuccessMessage };
            return RedirectToAction("Index");
        }

        [ActionName("StoreDeviceNaming")]
        public ActionResult StoreDeviceNaming(object id)
        {
            //make sure that delivery note is in Techinical approval
            //for test

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
            DeliveryNoteDetailsViewModel Model = null;
            if (items != null && items.Count > 0)
            {
                Model = items.SingleOrDefault();

                //get delivery details
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;

                //get delivery technicians
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryTechnicians = new DeliveryTechnicianModel<DeliveryTechnicianViewModel>().GetView<DeliveryTechnicianViewModel>(requestBody).PageItems;

                //get delivery devices
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "TransactionId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
            }
            ViewBag.NamingTypes = new DeviceNamingTypeModel<DeviceNamingType>().Get().Select(x => new SelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.DeviceNamingType_en, x.DeviceNamingType_ar), Value = x.DeviceNamingTypeId.ToString() }).ToList();
            return View(Model);
        }

        [HttpPost]
        public ActionResult StoreDeviceNaming(IEnumerable<DeliveryDeviceEditBindModel> DeliveryDevices, FormCollection fc)
        {
            //get delivery device
            string deliveryNoteId = fc["TransactionId"];

            //get delivery devices
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "TransactionId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryNoteId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var oldDeliveryDevices = new DeliveryItemModel<DeliveryItemViewModel>().GetView<DeliveryItemViewModel>(requestBody).PageItems;
            List<TransactionItem> UpdatedDeliveryDevice = new List<TransactionItem>();
            foreach (var oldDevice in oldDeliveryDevices)
            {
                TransactionItem deliveryDevice = new TransactionItem();

                //set old info
                Classes.Utilities.Utility.CopyObject<TransactionItem>(oldDevice, ref deliveryDevice);

                //set new info
                var newDeliveryDevice = DeliveryDevices.Where(x => x.TransactionItemId == oldDevice.TransactionItemId).SingleOrDefault();
                deliveryDevice.AddToServer = newDeliveryDevice.AddToServer;
                deliveryDevice.TrackWithTechnician = newDeliveryDevice.TrackWithTechnician;
                deliveryDevice.ServerUpdated = newDeliveryDevice.ServerUpdated;
                deliveryDevice.DeviceNaming_en = newDeliveryDevice.DeviceNaming_en;
                deliveryDevice.DeviceNaming_ar = newDeliveryDevice.DeviceNaming_ar;
                if(!string.IsNullOrEmpty(deliveryDevice.DeviceNaming_en) || !string.IsNullOrEmpty(deliveryDevice.DeviceNaming_ar))
                {
                    deliveryDevice.DeviceNamingTypeId = newDeliveryDevice.DeviceNamingTypeId;
                }
                deliveryDevice.Note = newDeliveryDevice.Note;
                deliveryDevice.ModifyUserId = User.UserId;
                deliveryDevice.ModifyDate = DateTime.Now;

                UpdatedDeliveryDevice.Add(deliveryDevice);
            }
            //update delivery devices
            List<UpdateItemFormat<TransactionItem>> items = UpdatedDeliveryDevice.Select(x => new UpdateItemFormat<TransactionItem>() { id = x.TransactionItemId, newValue = x }).ToList();
            var updatedItems = new DeliveryItemModel<TransactionItem>().Update(items);

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = updatedItems.Count, MessageContent = Resources.ServerManagement.DeliveryDeviceAddedToServerSuccessMessage };
            return RedirectToAction("Index");
        }

        [ActionName("Partial_DeliveryDetails")]
        public PartialViewResult Partial_DeliveryDetails (string id = null, DeliveryNoteDetailsViewModel deliveryNote = null)
        {
            if(!string.IsNullOrEmpty(id) && deliveryNote == null)
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
        public override ActionResult Create(DeliveryNoteCreateBindModel model)
        {
            return base.Create(model);
        }
        #endregion
    }
}