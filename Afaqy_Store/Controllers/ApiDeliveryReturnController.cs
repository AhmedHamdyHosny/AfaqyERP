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
    public class ApiDeliveryReturnController : BaseApiController<Transaction>
    {
        public ApiDeliveryReturnController()
        {
            var dataContrains = GetDataConstrains();
            if (dataContrains == null)
            {
                dataContrains = x => x.TransactionTypeId == (int)DBEnums.TransactionType.Delivery_Return;
            }
            else
            {
                dataContrains.AndAlso(x => x.TransactionTypeId == (int)DBEnums.TransactionType.Delivery_Return);
            }
            SetDataConstrains(dataContrains);

        }
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryReturnViewController();
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
        public IHttpActionResult PostDelivery(DeliveryReturnCreateBindModel value)
        {
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Post))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }

            const string SqlServerDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            //map delivery return to user defined dataTable 
            Classes.DB_UserTypeDefined.UDTransaction deliveryReturn = new Classes.DB_UserTypeDefined.UDTransaction();
            deliveryReturn.dataTable.Rows.Add(deliveryReturn.BindDataRow(value, SqlServerDateTimeFormat));
            
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
                ParameterName = "@salesmanId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = value.DolphinTrans.tra_sal_aux_id
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
                ParameterName = "@tbl_DeliveryReturn",
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                Value = deliveryReturn.dataTable
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
            
            using (var context = new AfaqyStoreEntities())
            {
                try
                {
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                    DataSet ds = new DataSet();
                    using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Classes.Utilities.SiteConfig.Connections.DolphinConnection)) //
                    {
                        command.CommandText = @"[afqy].[sp_InsertDolphinDeliveryReturn]";
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
                
            }
            
            return Content(HttpStatusCode.OK, ""); 

        }
        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryReturnViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiDeliveryReturnViewController : BaseApiController<TransactionView>
    {

    }
}
