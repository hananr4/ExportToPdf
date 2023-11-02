using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraReports.UI;
using ExportToPdf.Module.BusinessObjects;
using ExportToPdf.Module.Parameters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ExportToPdf.Module.Controllers;
public class ExportCustomerViewController : CustomerViewController
{
    readonly IReportExportService reportExportService;

    [ActivatorUtilitiesConstructor]
    public ExportCustomerViewController(IServiceProvider serviceProvider) : base()
    {
        reportExportService = serviceProvider.GetRequiredService<IReportExportService>();
    }
    public ExportCustomerViewController() { }

    protected override void PrintReport(string reportDisplayName, Customer customer, CustomizePopupWindowParamsEventArgs e)
    {
        using XtraReport report = reportExportService.LoadReport<ReportDataV2>(r => r.DisplayName == reportDisplayName);
        
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
