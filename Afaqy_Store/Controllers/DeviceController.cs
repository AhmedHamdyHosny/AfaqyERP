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
    public class DeviceController : BaseController<Device,DeviceViewModel,DeviceIndexViewModel,DeviceDetailsViewModel, DeviceCreateBindModel, DeviceEditBindModel, DeviceEditModel, DeviceImportModel, DeviceModel<Device>,DeviceModel<DeviceViewModel>>
    {
        
        //private List<DataLayer.ServerUnit> ServerUnits = null;
        //private List<DataLayer.TempDevice> TempDevices = null;
        public override void FuncPreDetailsView(object id, ref List<DeviceDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceId", Operation = GenericDataFormat.FilterOperations.Equal,Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new DeviceModel<DeviceDetailsViewModel>().GetView<DeviceDetailsViewModel>(requestBody).PageItems;
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            List<DeviceModelType> deviceModelTypes =  new DeviceModelTypeModel<DeviceModelType>().GetAsDDLst("DeviceModelTypeId,DeviceModelTypeName", "DeviceModelTypeName");
            ViewBag.DeviceModelTypeId = deviceModelTypes.Select(x=> new Classes.Helper.CustomSelectListItem() { Text = x.DeviceModelTypeName, Value = x.DeviceModelTypeId.ToString()});
        }
        public override void FuncPreCreate(ref DeviceCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.DeviceStatusId = (int)DBEnums.DeviceStatus.New;
            //for test
            model.BranchId = 1;
            
        }
        public override void FuncPreInitEditView(object id, ref Device EditItem, ref DeviceEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new DeviceModel<Device>().Get(id);
            }
            if (EditItem != null)
            {
                model = new DeviceEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<DeviceModelType> deviceModelTypes = new DeviceModelTypeModel<DeviceModelType>().GetAsDDLst("DeviceModelTypeId,DeviceModelTypeName", "DeviceModelTypeName");
                model.DeviceModelType = deviceModelTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.DeviceModelTypeName, Value = x.DeviceModelTypeId.ToString(), Selected = (selectedItem.DeviceModelTypeId == x.DeviceModelTypeId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref DeviceEditBindModel EditItem)
        {
            id = EditItem.DeviceId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Devices.xlsx";
            string properties = string.Join(",", typeof(DeviceView).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar")));
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

        public override ActionResult Import(FormCollection fc)
        {
            return base.ImportAsEntities(fc);
        }

        public override void FuncPreImportAsEntities(ref List<DeviceImportModel> items, FormCollection formCollection)
        {

            //for test
            //int branchId = GetBranch;
            int branchId = 1;
            items = items.Select(x =>
            {
                x.CreateUserId = User.UserId;
                x.CreateDate = DateTime.Now;
                x.DeviceStatusId = (int)Classes.Common.DBEnums.DeviceStatus.New;
                //x.IMEI = x.DeviceSerial.Length > 9 ? x.DeviceSerial : null;
                //x.SerialNumber = x.DeviceSerial.Length <= 9 ? x.DeviceSerial : null;
                x.BranchId = branchId;
                return x;
            }).ToList();
            
            //set device serial & device IMEI
            //items = items.Select(x =>
            //{
            //    x.SerialNumber = x.SerialNumber == null ? GetSerial(ref x) : x.SerialNumber;
            //    x.IMEI = x.IMEI == null ? GetIMEI(ref x) : x.IMEI;
            //    return x;
            //}).ToList() ;
            
        }

        //private string GetSerial(ref DeviceImportModel device)
        //{
        //    string imei = device.IMEI;
        //    string serialNumber = null;
        //    string note = "";
        //    List<string> SN_lst = new List<string>();
        //    List<string> tmp_SN_lst = new List<string>();
        //    List<string> server_SN_lst = new List<string>();
        //    if(!string.IsNullOrEmpty(imei))
        //    {
        //        //get serial number for server temp data
        //        if(TempDevices == null)
        //        {
        //            TempDevices = new TempDeviceModel<DataLayer.TempDevice>().Get();
        //        }
        //        var tempServerDevices = TempDevices.Where(x => x.DeviceIMEI == imei).ToList();
        //        if(tempServerDevices != null && tempServerDevices.Count > 0)
        //        {
        //            tmp_SN_lst = tempServerDevices.Where(x => x.DeviceSerial != null).Select(x => x.DeviceSerial).ToList();
        //            SN_lst.AddRange(tmp_SN_lst);
        //        }
        //        else
        //        {
        //            note = "Serial number of Device IMEI ["+imei+"] not exist in Stored Data (Excel Sheets)";
        //        }

        //        //get serial number for devices connected to server
        //        if (ServerUnits == null)
        //        {
        //            ServerUnits = new ServerUnitModel<DataLayer.ServerUnit>().Get();
        //        }

        //        var serverdevices = ServerUnits.Where(x => x.DeviceIMEI == imei).ToList();
        //        if(serverdevices != null && serverdevices.Count > 0)
        //        {
        //            //get serial number
        //            server_SN_lst = serverdevices.Where(x => x.DeviceSerial != null).Select(x => x.DeviceSerial).ToList();
        //            SN_lst.AddRange(server_SN_lst);
        //        }
        //        else
        //        {
        //            note = "Serial number of Device IMEI [" + imei + "] not exist in fetch Data from server api";
        //        }
        //        //remove all null or empty serial
        //        tmp_SN_lst = tmp_SN_lst.Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        //        server_SN_lst = server_SN_lst.Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        //        SN_lst = SN_lst.Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)).Distinct().ToList();

        //        //get serial number
        //        if(SN_lst.Count > 0)
        //        {
        //            if (SN_lst.Any(x => x == SN_lst[0]))
        //            {
        //                serialNumber = SN_lst[0];
        //            }
        //            else
        //            {
        //                note = "Serial number is difference between device in server [" + string.Join(",", server_SN_lst.ToArray()) + "] and device in stored data [" + string.Join(",", tmp_SN_lst.ToArray()) + "]";
        //                if(note.Length > 200)
        //                {
        //                    note = "Serial number is difference between device in server and device in stored data"; 
        //                }
        //            }
        //        }
        //        else if(server_SN_lst.Count == 0 && tmp_SN_lst.Count == 0)
        //        {
        //            note = "Serial number is difference between device in server [" + string.Join(",", server_SN_lst.ToArray()) + "] and device in stored data [" + string.Join(",", tmp_SN_lst.ToArray()) + "]";
        //            if (note.Length > 200)
        //            {
        //                note = "Serial number is difference between device in server and device in stored data";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        note = "Device IMEI is null or empty";
        //    }

        //    if(string.IsNullOrEmpty(serialNumber))
        //    {
        //        device.Note = note;
        //    }
        //    return serialNumber;
        //}
        //private string GetIMEI(ref DeviceImportModel device)
        //{
        //    string imei = null;
        //    string serialNumber = device.SerialNumber;
        //    string note = "";
        //    List<string> imei_lst = new List<string>();
        //    List<string> tmp_imei_lst = new List<string>();
        //    List<string> server_imei_lst = new List<string>();
        //    if (!string.IsNullOrEmpty(serialNumber))
        //    {
        //        //get IMEI for server temp data
        //        if (TempDevices == null)
        //        {
        //            TempDevices = new TempDeviceModel<DataLayer.TempDevice>().Get();
        //        }
        //        var tempServerDevices = TempDevices.Where(x => x.DeviceSerial == serialNumber).ToList();
        //        if (tempServerDevices != null && tempServerDevices.Count > 0)
        //        {
        //            tmp_imei_lst = tempServerDevices.Where(x => x.DeviceIMEI != null).Select(x => x.DeviceIMEI).ToList();
        //            imei_lst.AddRange(tmp_imei_lst);
        //        }
        //        else
        //        {
        //            note = "IMEI of Device Serial [" + serialNumber + "] not exist in Stored Data (Excel Sheets)";
        //        }

        //        //get IMEI for devices connected to server
        //        if (ServerUnits == null)
        //        {
        //            ServerUnits = new ServerUnitModel<DataLayer.ServerUnit>().Get();
        //        }

        //        var serverdevices = ServerUnits.Where(x => x.DeviceSerial == serialNumber).ToList();
        //        if (serverdevices != null && serverdevices.Count > 0)
        //        {
        //            //get IMEI
        //            server_imei_lst = serverdevices.Where(x => x.DeviceIMEI != null).Select(x => x.DeviceIMEI).ToList();
        //            imei_lst.AddRange(server_imei_lst);
        //        }
        //        else
        //        {
        //            note = "IMEI of Device Serial [" + serialNumber + "] not exist in fetch Data from server api";
        //        }

        //        //remove all null or empty serial
        //        tmp_imei_lst = tmp_imei_lst.Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        //        server_imei_lst = server_imei_lst.Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        //        imei_lst = imei_lst.Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)).Distinct().ToList();

        //        //get serial number
        //        if (imei_lst.Count > 0)
        //        {
        //            if (imei_lst.Any(x => x == imei_lst[0]))
        //            {
        //                imei = imei_lst[0];
        //            }
        //            else
        //            {
        //                note = "IMEI is difference between device in server [" + string.Join(",", server_imei_lst.ToArray()) + "] and device in stored data [" + string.Join(",", tmp_imei_lst.ToArray()) + "]";
        //                if (note.Length > 200)
        //                {
        //                    note = "IMEI is difference between device in server and device in stored data";
        //                }
        //            }
        //        }
        //        else if (server_imei_lst.Count == 0 && tmp_imei_lst.Count == 0)
        //        {
        //            note = "IMEI of Device Serial [" + serialNumber + "] not exist in fetch Data from server api or in Stored Data (Excel Sheets)";
        //        }
        //    }
        //    else
        //    {
        //        note = "Device Serial Number is null or empty";
        //    }
        //    if (string.IsNullOrEmpty(imei))
        //    {
        //        device.Note = note;
        //    }

        //    return imei;
        //}
    }
}