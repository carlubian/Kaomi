using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Loader;

namespace Kaomi.Core
{
    public class KaomiLoader
    {
        private static IDictionary<string, Assembly> asms = new Dictionary<string, Assembly>();
        private static IDictionary<string, KaomiTaskHost> prcs = new Dictionary<string, KaomiTaskHost>();

        public static void PullFromUri(string asmName, Uri uri)
        {
            using (var web = new WebClient())
                web.DownloadFile(uri, asmName);
        }

        public static string Load(string path)
        {
            var context = new KaomiLoadContext();

            Assembly asm;
            using (var stream = File.OpenRead(path))
                asm = context.LoadFromStream(stream);

            var id = asm.GetName().Name;

            if (asms.ContainsKey(id))
                asms.Remove(id);

            asms.Add(id, asm);

            return id;
        }

        public static IEnumerable<string> List()
        {
            return asms.Keys;
        }

        public static void Unload(string id)
        {
            var asm = asms[id];

            if (asm is null)
                return;

            asms.Remove(id);

            AssemblyLoadContext.GetLoadContext(asm).Unload();
        }

        public static void InstanceProcess(string id, string type)
        {
            var asm = asms[id];

            if (asm is null)
                return;

            var msg = asm.GetTypes().First(t => t.Name.Contains(type));
            var obj = Activator.CreateInstance(msg) as KaomiProcess;

            prcs.Add(type, new KaomiTaskHost(id, obj));
        }

        public static IEnumerable<string> ListProcesses()
        {
            return prcs.Keys;
        }
    }
}
