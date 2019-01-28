using Kaomi.Core.IO;
using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Kaomi.Core
{
    public static class KaomiLoader
    {
        internal static IDictionary<string, Assembly> asms = new Dictionary<string, Assembly>();
        internal static IDictionary<string, KaomiTaskHost> prcs = new Dictionary<string, KaomiTaskHost>();

        static KaomiLoader()
        {
            // Start system processes
            prcs.Add("MonitorFinishedProcesses", new KaomiTaskHost("System", new MonitorFinishedProcesses()));
            prcs.Add("AutoStartProcesses", new KaomiTaskHost("System", new AutoStartProcesses()));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsActive() => true;

        public static void PullFromUri(string asmName, Uri uri)
        {
            using (var web = new WebClient())
                web.DownloadFile(uri, asmName);

            Zip.ExtractFile(asmName);
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
            return prcs.Select(kvp => $"[{kvp.Value.AssemblyId}] {kvp.Key}");
        }

        public static bool HasResults(string processId)
        {
            return prcs[processId].Results.Count > 0;
        }

        public static IEnumerable<string> GetResults(string processId)
        {
            while (prcs[processId].Results.Count > 0)
                yield return prcs[processId].Results.Dequeue();
        }

        public static void SendMessage(string processId, string message)
        {
            if (prcs[processId].UserCommand.Count > 200)
                prcs[processId].UserCommand.Dequeue();
            prcs[processId].UserCommand.Enqueue(message);
        }
    }
}
