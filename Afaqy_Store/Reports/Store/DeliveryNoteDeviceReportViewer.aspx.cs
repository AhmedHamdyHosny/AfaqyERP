﻿using Afaqy_Store.DataLayer;
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
                string deliveryNoteId = Request.QueryString["DeliveryId"];
                //for test
                //string deliveryNoteId = "4";
                RenderReport(deliveryNoteId);
            }
        }

        private void RenderReport(string deliveryNoteId)
        {
            if(!string.IsNullOrEmpty(deliveryNoteId))
            {
                DataTable deviceDS = GetDeliveryDeviceDataTable(deliveryNoteId);
                DataTable deliveryDS = GetDeliveryNoteDataTable(deliveryNoteId);

                ReportDataSource deliveryDeviceDataSource = new ReportDataSource("DeliveryDeviceDataSet", deviceDS);
                ReportDataSource deliveryNoteDataSource = new ReportDataSource("DeliveryNoteDataSet", deliveryDS);

                InvoiceRptViewer.Reset();
                InvoiceRptViewer.LocalReport.EnableExternalImages = true;
                InvoiceRptViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Store/DeliveryNoteDevice.rdlc");
                //InvoiceRptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("",""));

                InvoiceRptViewer.LocalReport.DataSources.Add(deliveryNoteDataSource);
                InvoiceRptViewer.LocalReport.DataSources.Add(deliveryDeviceDataSource);
            }
        }

        private DataTable GetDeliveryDeviceDataTable(string deliveryNoteId)
        {
            //bind Delivery Device DataSet
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, Value = deliveryNoteId });
            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters };
            List<DeliveryDeviceView> deliveryDevice = new DeliveryDeviceModel<DeliveryDeviceView>().GetView<DeliveryDeviceView>(requestBody).PageItems;
            return Classes.Utilities.Utility.ToDataTable<DeliveryDeviceView>(deliveryDevice);
        }

        private DataTable GetDeliveryNoteDataTable(string deliveryNoteId)
        {
            //bind Delivery Note DataSet
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryNoteId", Operation = GenericDataFormat.FilterOperations.Equal, Value = deliveryNoteId });
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