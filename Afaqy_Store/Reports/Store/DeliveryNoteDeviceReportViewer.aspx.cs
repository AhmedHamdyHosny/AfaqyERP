using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using GenericApiController.Utilities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Afaqy_Store.Reports.Store
{
    public partial class DeliveryNoteDeviceReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                RenderReport();
            }
        }

        private void RenderReport()
        {
            DataTable deviceDS = GetDeliveryDeviceDataTable();
            DataTable deliveryDS = GetDeliveryNoteDataTable();

            ReportDataSource deliveryDeviceDataSource = new ReportDataSource("DeliveryDeviceDataSet", deviceDS);
            ReportDataSource deliveryNoteDataSource = new ReportDataSource("DeliveryNoteDataSet", deliveryDS);

            InvoiceRptViewer.Reset();
            InvoiceRptViewer.LocalReport.EnableExternalImages = true;
            InvoiceRptViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Store/DeliveryNoteDevice.rdlc");
            //InvoiceRptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("",""));

            InvoiceRptViewer.LocalReport.DataSources.Add(deliveryNoteDataSource);
            InvoiceRptViewer.LocalReport.DataSources.Add(deliveryDeviceDataSource);

        }

        private DataTable GetDeliveryDeviceDataTable()
        {
            //bind Delivery Device DataSet
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, Value = 4 });
            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters };
            List<DeliveryDeviceView> deliveryDevice = new DeliveryDeviceModel<DeliveryDeviceView>().GetView<DeliveryDeviceView>(requestBody).PageItems;
            return Classes.Utilities.Utility.ToDataTable<DeliveryDeviceView>(deliveryDevice);
        }

        private DataTable GetDeliveryNoteDataTable()
        {
            //bind Delivery Note DataSet
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, Value = 4 });
            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters };
            List<DeliveryNoteView> deliveryNote = new DeliveryNoteModel<DeliveryNoteView>().GetView<DeliveryNoteView>(requestBody).PageItems;
            return Classes.Utilities.Utility.ToDataTable<DeliveryNoteView>(deliveryNote);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //PrintReportSample.ReportPrintDocument rp = new PrintReportSample.ReportPrintDocument(InvoiceRptViewer.LocalReport);
            //rp.Print();
        }
    }
}