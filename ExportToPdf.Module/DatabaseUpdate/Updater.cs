using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ExportToPdf.Module.BusinessObjects;

namespace ExportToPdf.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        Customer customer = ObjectSpace.FirstOrDefault<Customer>(u => true);
        if (customer == null)
        {
            customer = ObjectSpace.CreateObject<Customer>();
            customer.FirstName = "Name 1";
            customer.Save();

            customer = ObjectSpace.CreateObject<Customer>();
            customer.FirstName = "Name 2";
            customer.Save();

            customer = ObjectSpace.CreateObject<Customer>();
            customer.FirstName = "Name 3";
            customer.Save();
        }

        ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
