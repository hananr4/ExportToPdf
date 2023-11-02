using System;
using System.Linq;

namespace ExportToPdf.Module.Parameters;

public class InMemoryFile
{
    public string FileName { get; set; }
    public byte[] Content { get; set; }
}
