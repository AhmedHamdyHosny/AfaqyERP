using Afaqy_Store.DataLayer;
using Classes.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiDeliveryNoteController : BaseApiController<DeliveryNote>
    {
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryNoteViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
        [NonAction]
        public override IHttpActionResult Post(DeliveryNote value)
        {
            return base.Post(value);
        }
        [ActionName("Post")]
        public IHttpActionResult PostDelivery(Models.DeliveryNoteCreateBindModel value)
        {

            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Post))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }

            //map delivery note to user defined dataTable 
            Classes.DB_UserTypeDefined.UDDeliveryNote delivery = new Classes.DB_UserTypeDefined.UDDeliveryNote();
            DataRow deliveryRow = delivery.dataTable.NewRow();
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryNoteId.ToString()] = value.DeliveryNoteId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.cmp_seq.ToString()] = value.cmp_seq;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.POS_ps_code.ToString()] = value.POS_ps_code;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.Warehouse_wa_code.ToString()] = value.Warehouse_wa_code;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryRequestId.ToString()] = value.DeliveryRequestId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.Customer_aux_id.ToString()] = value.Customer_aux_id;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.CustomerName.ToString()] = value.CustomerName;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.CustomerContact_serial.ToString()] = value.CustomerContact_serial;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.AlternativeContactName.ToString()] = value.AlternativeContactName;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.AlternativeContactTelephone.ToString()] = value.AlternativeContactTelephone;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.SaleTransactionTypeId.ToString()] = value.SaleTransactionTypeId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryDateTime.ToString()] = value.DeliveryDateTime;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryStatusId.ToString()] = value.DeliveryStatusId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryNoteReference.ToString()] = value.DeliveryNoteReference;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DolphinDelivery_tra_ref_id.ToString()] = value.DolphinDelivery_tra_ref_id;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.DolphinDelivery_tra_ref_type.ToString()] = value.DolphinDelivery_tra_ref_type;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.SystemId.ToString()] = value.SystemId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.WithInstallationService.ToString()] = value.WithInstallationService;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.Note.ToString()] = value.Note;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.IsBlock.ToString()] = value.IsBlock;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.CreateUserId.ToString()] = value.CreateUserId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.CreateDate.ToString()] = value.CreateDate;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.ModifyUserId.ToString()] = value.ModifyUserId;
            deliveryRow[DBEnums.UDDeliveryNote_DT_ColumnsName.ModifyDate.ToString()] = value.ModifyDate;
            delivery.dataTable.Rows.Add(deliveryRow);
            
            //map delivery technician to user defined dataTable
            Classes.DB_UserTypeDefined.UDDeliveryTechnician technicians = new Classes.DB_UserTypeDefined.UDDeliveryTechnician();
            foreach (var technician in value.DeliveryTechnician)
            {
                DataRow row = technicians.dataTable.NewRow();
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.DeliveryNoteTechnicalId.ToString()] = technician.DeliveryNoteTechnicalId;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.cmp_seq.ToString()] = technician.cmp_seq;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.Employee_aux_id.ToString()] = technician.Employee_aux_id;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.IsBlock.ToString()] = technician.IsBlock;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.CreateUserId.ToString()] = technician.CreateUserId;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.CreateDate.ToString()] = technician.CreateDate;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.ModifyUserId.ToString()] = technician.ModifyUserId;
                row[DBEnums.UDDeliveryTechnician_DT_ColumnsName.ModifyDate.ToString()] = technician.ModifyDate;
                technicians.dataTable.Rows.Add(row);
            }

            //map delivery details to user defined dataTable
            Classes.DB_UserTypeDefined.UDDeliveryDetails deliveryDetails = new Classes.DB_UserTypeDefined.UDDeliveryDetails();
            int detialsRowSerial = 1;
            foreach (var details in value.DeliveryDetails)
            {
                DataRow row = deliveryDetails.dataTable.NewRow();
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.DeliveryDetailsId.ToString()] = detialsRowSerial;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.cmp_seq.ToString()] = details.cmp_seq;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.ModelType_ia_item_id.ToString()] = details.ModelType_ia_item_id;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.Quantity.ToString()] = details.Quantity;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.Note.ToString()] = details.Note;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.IsBlock.ToString()] = details.IsBlock;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.CreateUserId.ToString()] = details.CreateUserId;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.CreateDate.ToString()] = details.CreateDate;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.ModifyUserId.ToString()] = details.ModifyUserId;
                row[DBEnums.UDDeliveryDetails_DT_ColumnsName.ModifyDate.ToString()] = details.ModifyDate;
                deliveryDetails.dataTable.Rows.Add(row);
                detialsRowSerial++;
            }

            //map delivery devices to user defined dataTable
            Classes.DB_UserTypeDefined.UDDeliveryDevice deliveryDevices = new Classes.DB_UserTypeDefined.UDDeliveryDevice();
            foreach (var device in value.DeliveryDevice)
            {
                DataRow row = deliveryDevices.dataTable.NewRow();
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.DeliveryItemId.ToString()] = device.DeliveryItemId;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.cmp_seq.ToString()] = value.cmp_seq;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.DeviceId.ToString()] = device.DeviceId;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.SerialNumber.ToString()] = device.SerialNumber;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.IMEI.ToString()] = device.IMEI;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.ModelType_ia_item_id.ToString()] = device.ModelType_ia_item_id;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.Employee_aux_id.ToString()] = device.Employee_aux_id;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.InstallingDateTime.ToString()] = device.InstallingDateTime;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.CarPlateNum.ToString()] = device.CarPlateNum;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.Note.ToString()] = device.Note;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.IsBlock.ToString()] = device.IsBlock;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.CreateUserId.ToString()] = value.CreateUserId;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.CreateDate.ToString()] = value.CreateDate;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.ModifyUserId.ToString()] = value.ModifyUserId;
                row[DBEnums.UDDeliveryDevice_DT_ColumnsName.ModifyDate.ToString()] = value.ModifyDate;
                deliveryDevices.dataTable.Rows.Add(row);
            }


            var sqlParams = new List<System.Data.SqlClient.SqlParameter>();
            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@cmp_seq",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.cmp_seq
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@trans_datetime",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                Value = value.DeliveryDateTime
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@pos_code",
                SqlDbType = SqlDbType.Char,
                Size = 3,
                Direction = ParameterDirection.Input,
                Value = value.POS_ps_code
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@warehouse_code",
                SqlDbType = SqlDbType.Char,
                Size = 3,
                Direction = ParameterDirection.Input,
                Value = value.Warehouse_wa_code
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@salesmanId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_sal_aux_id
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@customerId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.Customer_aux_id
            });

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

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@trans_ref",
                SqlDbType = SqlDbType.VarChar,
                Size = 15,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_sup_ref
            });

            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tra_ref_type",
                SqlDbType = SqlDbType.VarChar,
                Size = 15,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_ref_type
            });

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
                ParameterName = "@tbl_DeliveryDevice",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = deliveryDevices.dataTable
            });

            
            sqlParams.Add(new System.Data.SqlClient.SqlParameter()
            {
                ParameterName = "@tbl_DeliveryTechnician",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = technicians.dataTable
            });
            
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
            return Content(HttpStatusCode.OK, ""); //Content(HttpStatusCode.OK, result);

        }
        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryNoteViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiDeliveryNoteViewController : BaseApiController<DeliveryNoteView>
    {

    }

    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this System.Data.Entity.DbContext db, string storedProcedure)
        {
            return new MultipleResultSetWrapper(db, storedProcedure);
        }

        public class MultipleResultSetWrapper
        {
            private readonly System.Data.Entity.DbContext _db;
            private readonly string _storedProcedure;
            public List<Func<System.Data.Entity.Infrastructure.IObjectContextAdapter, System.Data.Common.DbDataReader, System.Collections.IEnumerable>> _resultSets;

            public MultipleResultSetWrapper(System.Data.Entity.DbContext db, string storedProcedure)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _resultSets = new List<Func<System.Data.Entity.Infrastructure.IObjectContextAdapter, System.Data.Common.DbDataReader, System.Collections.IEnumerable>>();
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }

            public List<System.Collections.IEnumerable> Execute()
            {
                var results = new List<System.Collections.IEnumerable>();

                using (var connection = _db.Database.Connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "EXEC " + _storedProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        var adapter = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_db);
                        foreach (var resultSet in _resultSets)
                        {
                            results.Add(resultSet(adapter, reader));
                            reader.NextResult();
                        }
                    }

                    return results;
                }
            }
        }
    }
}
