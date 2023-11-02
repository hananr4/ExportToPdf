﻿using System;
using System.IO.Compression;
using System.Linq;

namespace ExportToPdf.Module.Parameters;

public class HelperZip
{

    public static byte[] GetZipArchive(List<InMemoryFile> files)
    {
        byte[] archiveFile;
        using (var archiveStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                    using var zipStream = zipArchiveEntry.Open();
                    zipStream.Write(file.Content, 0, file.Content.Length);
                }
            }

            archiveFile = archiveStream.ToArray();
        }

        return archiveFile;
    }
}
