using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ExportToPdf.Module.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportToPdf.Module.Controllers
{
    public partial class CustomerViewController : ViewController
    {
        PopupWindowShowAction ExportPdfAction;
        public CustomerViewController()
        {
            InitializeComponent();
            ExportPdfAction = new PopupWindowShowAction(this, "ExportPdfAction", "View") { 
                Caption = "Export PDF",
                ImageName = "Action_Export_ToPDF",
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(BusinessObjects.Customer),
            };
            ExportPdfAction.CustomizePopupWindowParams += ExportPdf_CustomizePopupWindowParams;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            
        }
   
        private void ExportPdf_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var os = Application.CreateObjectSpace(typeof(ExportZipParameter));
            var parameter = os.CreateObject<ExportZipParameter>();

            var detailView = Application.CreateDetailView(os, parameter);   
            detailView.ViewEditMode = ViewEditMode.View;
            e.View = detailView;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
