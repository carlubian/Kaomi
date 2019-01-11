using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Ionic.Zip;

namespace Kaomi.Core.IO
{
    internal static class Zip
    {
        internal static void ExtractFile(string filename)
        {
            if (!ZipFile.IsZipFile(filename))
                return;

            using (var file = ZipFile.Read(filename))
                file.ExtractAll(Environment.CurrentDirectory, ExtractExistingFileAction.OverwriteSilently);
        }
    }
}
