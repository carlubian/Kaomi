using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Ionic.Zip;

namespace Kaomi.Core.IO
{
    /// <summary>
    /// Allows the Kaomi server to interact
    /// with Zip files.
    /// </summary>
    internal static class Zip
    {
        /// <summary>
        /// Attempts to extract downloaded files
        /// into the local directory.
        /// </summary>
        /// <param name="filename"></param>
        internal static void ExtractFile(string filename)
        {
            if (!ZipFile.IsZipFile(filename))
                return;

            using (var file = ZipFile.Read(filename))
                file.ExtractAll(Environment.CurrentDirectory, ExtractExistingFileAction.OverwriteSilently);
        }
    }
}
