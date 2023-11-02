using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.MiddleTier;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using ExportToPdf.Module.BusinessObjects;
using ExportToPdf.Module.Parameters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportToPdf.Module.Controllers;

public class Customer2ViewController : CustomerViewController
{
    readonly IReportExportService reportExportService;

    [ActivatorUtilitiesConstructor]
    public Customer2ViewController(IServiceProvider serviceProvider) : base()
    {
        reportExportService = serviceProvider.GetRequiredService<IReportExportService>();
    }
    public Customer2ViewController() { }

    protected override void PrintReport(string reportDisplayName, Customer customer, CustomizePopupWindowParamsEventArgs e)
    {
        using XtraReport report = reportExportService.LoadReport<ReportDataV2>(r => r.DisplayName == reportDisplayName);
        // Filter and sort report data
        //CriteriaOperator objectsCriteria = ((BaseObjectSpace)ObjectSpace).GetObjectsCriteria(((ObjectView)View).ObjectTypeInfo, selectedObjects);
        //SortProperty[] sortProperties = { new SortProperty("Age", SortingDirection.Descending) };

        report.Parameters["oid"].Value = customer.Oid;


        reportExportService.SetupReport(report);

        using Stream s = reportExportService.ExportReport(report, DevExpress.XtraPrinting.ExportTarget.Pdf);
        var fileName = reportDisplayName + ".pdf";

        var os = Application.CreateObjectSpace(typeof(ExportFileParameter));
        var exportFileParameter = os.CreateObject<ExportFileParameter>();

        var os2 = Application.CreateObjectSpace(typeof(FileData));
        var fileData = os2.CreateObject<FileData>();

        fileData.LoadFromStream(fileName, s);
        exportFileParameter.File = fileData;

        os.CommitChanges();

        var view = Application.CreateDetailView(os, exportFileParameter);
        view.ViewEditMode = ViewEditMode.View;
        e.View = view;

    }
}

public abstract class CustomerViewController : ObjectViewController<ListView, Customer>
{
    private readonly PopupWindowShowAction printAction ;
    public CustomerViewController()
    {
        printAction = new PopupWindowShowAction(this, "Export PDF", PredefinedCategory.Reports) { 
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