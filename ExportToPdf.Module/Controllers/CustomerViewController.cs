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
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ExportPdfAction = new PopupWindowShowAction(this, "ExportPdfAction", "View");
            ExportPdfAction.CustomizePopupWindowParams += ExportPdf_CustomizePopupWindowParams;
            
        }
   
        private void ExportPdf_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
 
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
