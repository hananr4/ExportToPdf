using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using ExportToPdf.Module.BusinessObjects;
using System;
using System.Linq;

namespace ExportToPdf.Module.Controllers;

public abstract class CustomerViewController : ObjectViewController<ListView, Customer>
{
    private readonly PopupWindowShowAction printAction;
    public CustomerViewController()
    {
        printAction = new PopupWindowShowAction(this, "Export PDF", PredefinedCategory.Reports)
        {
            Caption = "PDF",
            ImageName = "Action_Printing_Print",
            ToolTip = "Export PDF",
            SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            TargetObjectsCriteria = "IsNewObject(This) = false"
        };
        printAction.CustomizePopupWindowParams += PrintAction_CustomizePopupWindowParams;
        ListView view = View;

    }
    void PrintAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
    {
        PrintReport("Customer Report", View.CurrentObject as Customer, e);
    }
    protected abstract void PrintReport(string reportDisplayName, Customer customer, CustomizePopupWindowParamsEventArgs e);
}