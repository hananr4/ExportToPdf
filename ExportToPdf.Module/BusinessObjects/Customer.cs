using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportToPdf.Module.BusinessObjects;

[DefaultClassOptions]
[VisibleInReports]
public class Customer : BaseObject
{
    public Customer(Session session) : base(session) { }
    public Customer() { }


    string _firstName;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string FirstName
    {
        get => _firstName;
        set => SetPropertyValue(nameof(FirstName), ref _firstName, value);
    }
}
