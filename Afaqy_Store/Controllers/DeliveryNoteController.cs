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
    public class DeliveryNoteController : BaseController<DeliveryNote,DeliveryNoteViewModel,DeliveryNoteIndexViewModel, DeliveryNoteDetailsViewModel, DeliveryNoteCreateBindModel, DeliveryNoteEditBindModel, DeliveryNoteEditModel, DeliveryNote, DeliveryNoteModel<DeliveryNote>, DeliveryNoteModel<DeliveryNoteViewModel>>
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
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeliveryNoteModel<DeliveryNoteDetailsViewModel>().GetView<DeliveryNoteDetailsViewModel>(requestBody).PageItems;

            foreach (var item in items)
            {
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, Value = item.DeliveryRequestId });
                var detailsRequestBody = new GenericDataFormat() { Filters = filters };
                item.DeliveryDetails = new DeliveryDetailsModel<DeliveryDetails_DetailsViewModel>().GetView<DeliveryDetails_DetailsViewModel>(requestBody).PageItems;
            }

        }

        [ActionName("Create")]
        public ActionResult CreateDelivery(int id)
        {
            var model = new DeliveryNoteCreateBindModel();
            model.DeliveryRequestId = id;
            FuncPreInitCreateView(ref model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult CreateDelivery(DeliveryNoteCreateBindModel model)
        {
            if (ModelState.IsValid)
            {
                FuncPreCreate(ref model);
                //create instance to insert object
                var instance = new DeliveryNoteModel<DeliveryNote>();
                var item = instance.Insert(model);
                
                return FuncPostCreate(ref model, ref item);
            }
            
            FuncPreInitCreateView(ref model);
            return View(model);
        }
        
        public void FuncPreInitCreateView(ref DeliveryNoteCreateBindModel model)
        {
            //get delivery details info
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestTechnician" } };
            var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();

            //get delivery details view info
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            requestBody = new GenericDataFormat() { Filters = filters};
            var deliveryRequestView = new DeliveryRequestModel<DeliveryRequestViewModel>().GetView<DeliveryRequestViewModel>(requestBody).PageItems.SingleOrDefault();

            //get delivery request details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = model.DeliveryRequestId });
            requestBody = new GenericDataFormat() { Filters = filters };
            var deliveryRequestDetails = new DeliveryRequestDetailsModel<DeliveryRequestDetailsView>().GetView<DeliveryRequestDetailsView>(requestBody).PageItems;
            
            model.DeliveryRequest = deliveryRequest;
            model.DeliveryRequestView = deliveryRequestView;
            model.DeliveryRequestDetails = deliveryRequestDetails;
            
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

            
            //get delivery request technician view
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            requestBody = new GenericDataFormat() { Filters = filters };
            model.DeliveryRequestTechnician = new DeliveryRequestTechnicianModel<DeliveryRequestTechnicianViewModel>().GetView<DeliveryRequestTechnicianViewModel>(requestBody).PageItems;

            //get delivery reference
            model.DeliveryNoteReference = model.GetNewDeliveryReference(model.DeliveryRequest.Warehouse_wa_code);
        }
        public override void FuncPreCreate(ref DeliveryNoteCreateBindModel model)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = model.DeliveryRequestId });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestTechnician" } };
            var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();

            model.ToDeliveryNote(deliveryRequest);
            model.DeliveryTechnician = deliveryRequest.DeliveryRequestTechnician.Select(x => new DeliveryTechnician() { cmp_seq = x.cmp_seq, Employee_aux_id = x.Employee_aux_id, CreateUserId = User.UserId, CreateDate = DateTime.Now }).ToList();

            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.DeliveryStatusId = (int)Classes.Common.DBEnums.DeliveryStatus.New;

            //get saleman id
            int? salesmanId = new CustomerModel<rpaux>().Get(model.Customer_aux_id).salecode;

            //set Dolphin Transaction
            model.DolphinTrans = new it_trans_a()
            {
                tra_sal_aux_id = salesmanId, //Saleman Id
                tra_cura_seq = 1, //Currency id (SAR)
                tra_status = "0", //initial status
                tra_user_id = User.DolphinUser, //dolphin user
                tra_sup_ref = model.DeliveryNoteReference, //Delivery Note Reference
                tra_ref_type = 5 //5 is delivery note type
            };


            var cmp_seq = model.cmp_seq;
            model.DeliveryDetails = model.DeliveryDetails.Select(x => { x.cmp_seq = cmp_seq; x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();
            foreach (var item in model.DeliveryDetails)
            {
                item.DeliveryDevice = model.DeliveryDevice.Where(x => x.ModelType_ia_item_id != null && x.ModelType_ia_item_id  == item.ModelType_ia_item_id)
                    .Select(x => new DeliveryDevice()
                    {
                        cmp_seq = cmp_seq,
                        DeliveryDetailsId = x.DeliveryDetailsId,
                        DeviceId = x.DeviceId,
                        Employee_aux_id = x.Employee_aux_id,
                        CreateUserId = User.UserId,
                        CreateDate = DateTime.Now
                    }).ToList();
            }
            
        }
        public override void FuncPreInitEditView(object id, ref DeliveryNote EditItem, ref DeliveryNoteEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
                var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryDetails,DeliveryDetails.DeviceModelType" } };
                EditItem = new DeliveryNoteModel<DeliveryNote>().Get(requestBody).SingleOrDefault();
            }
            if (EditItem != null)
            {
                model = new DeliveryNoteEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref DeliveryNoteEditBindModel EditItem)
        {
            id = EditItem.DeliveryNoteId;

            //get delivery request details 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails" } };
            var deliveryNote = new DeliveryNoteModel<DeliveryNote>().Get(requestBody).SingleOrDefault();
            var originalDeliveryDetails = deliveryNote.DeliveryDetails;
            foreach (var item in EditItem.DeliveryDetails)
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
            string properties = string.Join(",", typeof(DeliveryNote).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar"))); ;
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

        [ActionName("ServerAdd")]
        public ActionResult ServerAdd(object id)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal,
                Value = id , LogicalOperation = GenericDataFormat.LogicalOperations.And
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
                    Property = "DeliveryId",
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
                    Property = "DeliveryNoteId",
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
                    Property = "DeliveryNoteId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryDevices = new DeliveryDeviceModel<DeliveryDeviceViewModel>().GetView<DeliveryDeviceViewModel>(requestBody).PageItems;
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult ServerAdd(DeliveryDeviceEditBindModel[] DeliveryDevice, FormCollection fc)
        {
            //get delivery device
            string deliveryNoteId = fc["DeliveryNoteId"];

            //get delivery devices
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "DeliveryNoteId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryNoteId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var oldDeliveryDevices = new DeliveryDeviceModel<DeliveryDeviceViewModel>().GetView<DeliveryDeviceViewModel>(requestBody).PageItems;
            List<DeliveryDevice> UpdatedDeliveryDevice = new List<DeliveryDevice>();
            foreach (var oldDevice in oldDeliveryDevices)
            {
                DeliveryDevice deliveryDevice = new DeliveryDevice();

                //set old info
                deliveryDevice.DeliveryItemId = oldDevice.DeliveryItemId;
                deliveryDevice.cmp_seq = oldDevice.DeliveryDevice_cmp_seq;
                deliveryDevice.DeliveryDetailsId = oldDevice.DeliveryDetailsId;
                deliveryDevice.DeviceId = oldDevice.DeviceId;
                deliveryDevice.Employee_aux_id = oldDevice.Employee_aux_id;
                deliveryDevice.InstallingDateTime = oldDevice.InstallingDateTime;
                deliveryDevice.IsBlock = oldDevice.IsBlock;
                deliveryDevice.CreateUserId = oldDevice.CreateUserId;
                deliveryDevice.CreateDate = oldDevice.CreateDate;
                //set new info
                var newDeliveryDevice = DeliveryDevice.Where(x => x.DeliveryItemId == oldDevice.DeliveryItemId).SingleOrDefault();
                deliveryDevice.AddToServer = newDeliveryDevice.AddToServer;
                deliveryDevice.TrackWithTechnician = newDeliveryDevice.TrackWithTechnician;
                deliveryDevice.ServerUpdated = newDeliveryDevice.ServerUpdated;
                deliveryDevice.DeviceNaming = newDeliveryDevice.DeviceNaming;
                deliveryDevice.DeviceNamingTypeId = newDeliveryDevice.DeviceNamingTypeId;
                deliveryDevice.Note = newDeliveryDevice.Note;
                deliveryDevice.ModifyUserId = User.UserId;
                deliveryDevice.ModifyDate = DateTime.Now;

                UpdatedDeliveryDevice.Add(deliveryDevice);
            }
            //update delivery devices
            List<UpdateItemFormat<DeliveryDevice>> items = UpdatedDeliveryDevice.Select(x => new UpdateItemFormat<DeliveryDevice>() { id = x.DeliveryItemId, newValue = x }).ToList();
            var updatedItems = new DeliveryDeviceModel<DeliveryDevice>().Update(items);
            
            //send notification to users
            string referenceURL = this.Url.Action("Details");
            if (DeliveryDeviceEditBindModel.SendAddToServerNotification(deliveryNoteId, referenceURL))
            {
                //do nothing
            }

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = updatedItems.Count, MessageContent = Resources.ServerManagement.DeliveryDeviceAddedToServerSuccessMessage };
            return RedirectToAction("Index");
        }

        [ActionName("DeviceNaming")]
        public ActionResult DeviceNaming(object id)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "DeliveryNoteId",
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
                    Property = "DeliveryNoteId",
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
                    Property = "DeliveryNoteId",
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
                    Property = "DeliveryNoteId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = id,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                requestBody = new GenericDataFormat() { Filters = filters };
                Model.DeliveryDevices = new DeliveryDeviceModel<DeliveryDeviceViewModel>().GetView<DeliveryDeviceViewModel>(requestBody).PageItems;
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult DeviceNaming(IEnumerable<DeliveryDeviceEditBindModel> DeliveryDevices, FormCollection fc)
        {
            //get delivery device
            string deliveryNoteId = fc["DeliveryNoteId"];

            //get delivery devices
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "DeliveryNoteId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryNoteId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var oldDeliveryDevices = new DeliveryDeviceModel<DeliveryDeviceViewModel>().GetView<DeliveryDeviceViewModel>(requestBody).PageItems;
            List<DeliveryDevice> UpdatedDeliveryDevice = new List<DeliveryDevice>();
            foreach (var oldDevice in oldDeliveryDevices)
            {
                DeliveryDevice deliveryDevice = new DeliveryDevice();

                //set old info
                deliveryDevice.cmp_seq = oldDevice.DeliveryDevice_cmp_seq;
                deliveryDevice.DeliveryDetailsId = oldDevice.DeliveryDetailsId;
                deliveryDevice.DeviceId = oldDevice.DeviceId;
                deliveryDevice.Employee_aux_id = oldDevice.Employee_aux_id;
                deliveryDevice.InstallingDateTime = oldDevice.InstallingDateTime;
                deliveryDevice.IsBlock = oldDevice.IsBlock;
                deliveryDevice.CreateUserId = oldDevice.CreateUserId;
                deliveryDevice.CreateDate = oldDevice.CreateDate;
                //set new info
                var newDeliveryDevice = DeliveryDevices.Where(x => x.DeliveryItemId == oldDevice.DeliveryItemId).SingleOrDefault();
                deliveryDevice.AddToServer = newDeliveryDevice.AddToServer;
                deliveryDevice.TrackWithTechnician = newDeliveryDevice.TrackWithTechnician;
                deliveryDevice.ServerUpdated = newDeliveryDevice.ServerUpdated;
                deliveryDevice.DeviceNaming = newDeliveryDevice.DeviceNaming;
                deliveryDevice.DeviceNamingTypeId = newDeliveryDevice.DeviceNamingTypeId;
                deliveryDevice.Note = newDeliveryDevice.Note;
                deliveryDevice.ModifyUserId = User.UserId;
                deliveryDevice.ModifyDate = DateTime.Now;

                UpdatedDeliveryDevice.Add(deliveryDevice);
            }
            //update delivery devices
            List<UpdateItemFormat<DeliveryDevice>> items = UpdatedDeliveryDevice.Select(x => new UpdateItemFormat<DeliveryDevice>() { id = x.DeliveryItemId, newValue = x }).ToList();
            var updatedItems = new DeliveryDeviceModel<DeliveryDevice>().Update(items);

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = updatedItems.Count, MessageContent = Resources.ServerManagement.DeliveryDeviceAddedToServerSuccessMessage };
            return RedirectToAction("Index");
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