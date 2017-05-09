using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using Classes.Common;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiDeliveryNoteController : BaseApiController<Transaction>
    {
        public ApiDeliveryNoteController()
        {
            var dataContrains = GetDataConstrains();
            if (dataContrains == null)
            {
                dataContrains = x => x.TransactionTypeId == (int)DBEnums.TransactionType.Delivery_Note;
            }
            else
            {
                dataContrains.AndAlso(x => x.TransactionTypeId == (int)DBEnums.TransactionType.Delivery_Note);
            }
            SetDataConstrains(dataContrains);

        }
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryNoteViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
        [NonAction]
        public override IHttpActionResult Post(Transaction value)
        {
            return base.Post(value);
        }
        [ActionName("Post")]
        public IHttpActionResult PostDelivery(DeliveryNoteCreateBindModel value)
        {
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Post))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }

            const string SqlServerDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            //map delivery note to user defined dataTable 
            Classes.DB_UserTypeDefined.UDTransaction delivery = new Classes.DB_UserTypeDefined.UDTransaction();
            //DataRow deliveryRow = delivery.dataTable.NewRow();
            delivery.dataTable.Rows.Add(delivery.BindDataRow(value, SqlServerDateTimeFormat));
            
            //map delivery technician to user defined dataTable
            Classes.DB_UserTypeDefined.UDDeliveryTechnician technicians = new Classes.DB_UserTypeDefined.UDDeliveryTechnician();
            foreach (var technician in value.TransactionTechnician)
            {
                technicians.dataTable.Rows.Add(technicians.BindDataRow(technician, SqlServerDateTimeFormat));
            }

            //map delivery details to user defined dataTable
            Classes.DB_UserTypeDefined.UDDeliveryDetails deliveryDetails = new Classes.DB_UserTypeDefined.UDDeliveryDetails();
            int detialsRowSerial = 1;
            foreach (var details in value.TransactionDetails)
            {
                deliveryDetails.dataTable.Rows.Add(deliveryDetails.BindDataRow(details, SqlServerDateTimeFormat, detialsRowSerial));
                detialsRowSerial++;
            }

            //map delivery items to user defined dataTable
            Classes.DB_UserTypeDefined.UDDeliveryItem deliveryItems = new Classes.DB_UserTypeDefined.UDDeliveryItem();
            foreach (var device in value.DeliveryDevice)
            {
                deliveryItems.dataTable.Rows.Add(deliveryItems.BindDataRow(device, value.cmp_seq, value.CreateUserId, value.CreateDate, value.ModifyUserId, value.ModifyDate, SqlServerDateTimeFormat));
            }

            ////send notifications to users
            ////send delivery request new status
            //Classes.DB_UserTypeDefined.UDNotification notification = null;
            //List<int> UserIds = new List<int>();
            ////1. send notification to delivery request user and his manager
            //var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(value.DeliveryRequestId);
            ////add delivery request creator user
            //UserIds.Add(deliveryRequest.CreateUserId);
            ////add delivery request modifier user  if exist
            //if(deliveryRequest.ModifyUserId != null)
            //{
            //    UserIds.Add((int)deliveryRequest.ModifyUserId);
            //}
            ////add delivery request creator user manager
            //filters = new List<GenericDataFormat.FilterItems>();
            //filters.Add(new GenericDataFormat.FilterItems()
            //{
            //    Property = "UserId",
            //    Operation = GenericDataFormat.FilterOperations.Equal,
            //    Value = deliveryRequest.CreateUserId,
            //    LogicalOperation = GenericDataFormat.LogicalOperations.And
            //});
            //requestBody = new GenericDataFormat() { Filters = filters };
            //var creatorEmp = new EmployeeModel<Employee>().Get(requestBody).SingleOrDefault();
            //if(creatorEmp != null && creatorEmp.IsManager == false && creatorEmp.ManagerId != null)
            //{
            //    var managerEmp = new EmployeeModel<Employee>().Get(creatorEmp.ManagerId);
            //    if(managerEmp != null && managerEmp.UserId != null)
            //    {
            //        UserIds.Add((int)managerEmp.UserId);
            //    }
            //}
            
            ////add notification to table
            ////map notification to user defined dataTable
            //notification = new Classes.DB_UserTypeDefined.UDNotification();
            //DataRow notificationRow = null;
            //foreach (var userId in UserIds.Distinct())
            //{
            //    notificationRow = notification.dataTable.NewRow();
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationId.ToString()] = 0;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationTitle.ToString()] = Resources.Sales.DeliveryRequestInDeliveryPhaseNotificationTitle;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationContent.ToString()] = Resources.Sales.DeliveryRequestInDeliveryPhaseNotificationContent.Replace("@customer",deliveryRequest.CustomerName);
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.StyleClass.ToString()] = Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationType.ToString()] = null;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.ToUserId.ToString()] = userId;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.CreateDate.ToString()] = DateTime.Now.ToString(SqlServerDateTimeFormat);
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.IsRead.ToString()] = false;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationTypeId.ToString()] = (int)DBEnums.NotificationType.DeliveryRequest_InDeliveryPhaseNotification;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.ReferenceId.ToString()] = deliveryRequest.DeliveryRequestId;
            //    notificationRow[DBEnums.UDNotification_DT_ColumnsName.ReferenceLink.ToString()] = Classes.Utilities.SiteConfig.ApiUrl + "DeliveryRequest/Details/"+ deliveryRequest.DeliveryRequestId;
            //    notification.dataTable.Rows.Add(notificationRow);
            //}

            ////send new delivery note notification
            ////get all employee in Server management Department
            //filters = new List<GenericDataFormat.FilterItems>();
            //filters.Add(new GenericDataFormat.FilterItems()
            //{
            //    Property = "DepartmentId",
            //    Operation = GenericDataFormat.FilterOperations.Equal,
            //    Value = (int)DBEnums.Department.Server_Management,
            //    LogicalOperation = GenericDataFormat.LogicalOperations.And
            //});
            //filters.Add(new GenericDataFormat.FilterItems()
            //{
            //    Property = "IsBlock",
            //    Operation = GenericDataFormat.FilterOperations.Equal,
            //    Value = false,
            //    LogicalOperation = GenericDataFormat.LogicalOperations.And
            //});
            //requestBody = new GenericDataFormat()
            //{ Filters = filters };
            
            //var serverEmployees = new EmployeeModel<Employee>().Get(requestBody);
            //if(serverEmployees != null)
            //{
            //    foreach (var employee in serverEmployees)
            //    {
            //        if(employee.UserId != null)
            //        {
            //            //map notification to user defined dataTable
            //            notificationRow = notification.dataTable.NewRow();
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationId.ToString()] = 0;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationTitle.ToString()] = Resources.ServerManagement.NewDeliveryNoteNotificationTitle;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationContent.ToString()] = Resources.ServerManagement.NewDeliveryNoteNotificationContent;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.StyleClass.ToString()] = Constant.NotificationStyleClass.DeliveryNote_DefaultStyleClass;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationType.ToString()] = null;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.ToUserId.ToString()] = employee.UserId;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.CreateDate.ToString()] = DateTime.Now.ToString(SqlServerDateTimeFormat);
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.IsRead.ToString()] = false;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.NotificationTypeId.ToString()] = (int)DBEnums.NotificationType.DeliveryNote_AddedNotification;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.ReferenceId.ToString()] = 0;
            //            notificationRow[DBEnums.UDNotification_DT_ColumnsName.ReferenceLink.ToString()] = Classes.Utilities.SiteConfig.ApiUrl + "DeliveryNote/ServerRecevied/";
            //            notification.dataTable.Rows.Add(notificationRow);
            //        }
            //    }
            //}
            
            
            var sqlParams = new List<System.Data.SqlClient.SqlParameter>();
            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@cmp_seq",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.cmp_seq
            });

            //sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //{
            //    ParameterName = "@trans_datetime",
            //    SqlDbType = SqlDbType.DateTime,
            //    Direction = ParameterDirection.Input,
            //    Value = value.TransactionDateTime
            //});

            //sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //{
            //    ParameterName = "@pos_code",
            //    SqlDbType = SqlDbType.Char,
            //    Size = 3,
            //    Direction = ParameterDirection.Input,
            //    Value = value.POS_ps_code
            //});

            //sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //{
            //    ParameterName = "@warehouse_code",
            //    SqlDbType = SqlDbType.Char,
            //    Size = 3,
            //    Direction = ParameterDirection.Input,
            //    Value = value.Warehouse_wa_code
            //});

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@salesmanId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_sal_aux_id
            });

            //sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //{
            //    ParameterName = "@customerId",
            //    SqlDbType = SqlDbType.Int,
            //    Direction = ParameterDirection.Input,
            //    Value = value.Customer_aux_id
            //});

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@currencyId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_cura_seq
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tra_status",
                SqlDbType = SqlDbType.Char,
                Size = 1,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_status
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tra_user",
                SqlDbType = SqlDbType.VarChar,
                Size = 15,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_user_id
            });

            //sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //{
            //    ParameterName = "@trans_ref",
            //    SqlDbType = SqlDbType.VarChar,
            //    Size = 15,
            //    Direction = ParameterDirection.Input,
            //    Value = value.DolphinTrans.tra_sup_ref
            //});

            //sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //{
            //    ParameterName = "@tra_ref_type",
            //    SqlDbType = SqlDbType.VarChar,
            //    Size = 15,
            //    Direction = ParameterDirection.Input,
            //    Value = value.DolphinTrans.tra_ref_type
            //});

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@device_NewStatus",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = (int)DBEnums.DeviceStatus.In_customer_delivery_phase
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@deliveryRequest_NewStatus",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = DBEnums.DeliveryRequestStatus.In_the_delivery_phase
            });
            
            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tbl_DeliveryNote",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = delivery.dataTable
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tbl_DeliveryDetails",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = deliveryDetails.dataTable
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tbl_DeliveryItem",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = deliveryItems.dataTable
            });

            
            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tbl_DeliveryTechnician",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = technicians.dataTable
            });

            //if (notification != null)
            //{
            //    sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            //    {
            //        ParameterName = "@tbl_Notification",
            //        SqlDbType = SqlDbType.Structured,
            //        Direction = ParameterDirection.Input,
            //        Value = notification.dataTable
            //    });
            //}
            
            using (var context = new AfaqyStoreEntities())
            {
                try
                {
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                    DataSet ds = new DataSet();
                    using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Classes.Utilities.SiteConfig.Connections.DolphinConnection)) //
                    {
                        command.CommandText = @"[afqy].[sp_InsertDolphinDeliveryNote]";
                        command.Parameters.AddRange(sqlParams.ToArray());
                        command.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        command.Connection = con;
                        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(command);
                        da.Fill(ds);
                        con.Close();
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
                
                //try
                //{
                //    var result = context.Database.SqlQuery<DataTable>("EXECUTE [afqy].[sp_InsertDolphinDeliveryNote] " +
                //    "@cmp_seq, @trans_datetime, @pos_code, @warehouse_code, @salesmanId, @customerId, " +
                //    "@currencyId, @tra_status, @tra_user, @trans_ref, @tra_ref_type, @tbl_trans_details, " +
                //    "@tbl_trans_items_serial,@tbl_delivery_technician", sqlParams);
                //}
                //catch (Exception ex)
                //{

                //    throw ex;
                //}
                //context.sp_InsertDolphinDeliveryNote(value.cmp_seq, value.DeliveryDateTime,
                //    value.POS_ps_code, value.Warehouse_wa_code, value.DolphinTrans.tra_sal_aux_id,
                //    value.Customer_aux_id, value.DolphinTrans.tra_cura_seq,
                //    value.DolphinTrans.tra_status, value.DolphinTrans.tra_user_id,
                //    value.DolphinTrans.tra_sup_ref, value.DolphinTrans.tra_ref_type);
            }


            /*
            DeliveryNote result = null;
            using (var context = new AfaqyStoreEntities())
            {
                result = context.DeliveryNote.Add(value);

                var UserId = value.CreateUserId;
                //update status of each device in Delivery
                foreach (var item in value.DeliveryDetails.SelectMany(x => x.DeliveryDevice))
                {

                    var device = context.Device.SingleOrDefault(x => x.DeviceId == item.DeviceId);
                    //get current status of device
                    var lastStatus = device.DeviceStatusId;
                    //update device status
                    device.DeviceStatusId = (int)Classes.Common.DBEnums.DeviceStatus.In_customer_delivery_phase;
                    //if device has new status insert it to status history
                    if (lastStatus != device.DeviceStatusId)
                    {
                        //insert new status in status history
                        var CurrentUser = new Models.UserViewModel().GetUserFromSession();
                        var userId = CurrentUser.UserId;
                        context.DeviceStatusHistory.Add(new DeviceStatusHistory() { DeviceId = device.DeviceId, DeviceStatusId = device.DeviceStatusId, BranchId = device.BranchId, Note = value.Note, CreateUserId = userId, CreateDate = DateTime.Now });
                    }

                    context.Entry(device).State = System.Data.Entity.EntityState.Modified;
                }

                //insert delivery note to dolphin

                context.SaveChanges();
            }
            */
            Transaction result = new Transaction();
            //Classes.Utilities.DBExecuteResult<Transaction> result = new Classes.Utilities.DBExecuteResult<Transaction>() { Result = Classes.Utilities.DBExecuteResult.ExecuteResult.Success, ResultModel = insertedDeliveryNote };
            return Content(HttpStatusCode.OK, result);

        }
        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryNoteViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiDeliveryNoteViewController : BaseApiController<TransactionView>
    {

    }


}
