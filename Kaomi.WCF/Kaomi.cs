using DotNet.Misc.Extensions.Linq;
using Kaomi.Core.Model;
using Kaomi.WCF.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kaomi.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class Kaomi : MarshalByRefObject, IKaomi
    {
        private IDictionary<ProcessID, (AppDomain appDomain, KaomiProcess process)> Processes 
            = new LenientDictionary<ProcessID, (AppDomain appDomain, KaomiProcess process)>();

        /// <summary>
        /// Descarga un ensamblado desde una URI.
        /// </summary>
        /// <param name="assemblyName">Nombre del ensamblado</param>
        /// <param name="path">URI donde se encuentra el ensamblado</param>
        /// <returns>Ruta del ensamblado</returns>
        public FileInfo DownloadAssembly(string assemblyName, Uri path)
        {
            if (IsFile.BeingUsed(new FileInfo($"{assemblyName}.dll")))
                ; // TODO Descargar AppDomain

            using (var web = new WebClient())
                web.DownloadFile(path, $"{assemblyName}.dll");

            return new FileInfo($"{assemblyName}.dll");
        }

        /// <summary>
        /// Instancia el proceso indicado en un nuevo AppDomain.
        /// </summary>
        /// <param name="app">Ruta del ensamblado</param>
        /// <param name="type">Nombre del tipo</param>
        /// <returns>ID del proceso</returns>
        public ProcessID InstanceProcess(FileInfo app, string type)
        {
            string pathToDll = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, app.Name);
            var domainSetup = new AppDomainSetup { PrivateBinPath = pathToDll };
            var newDomain = AppDomain.CreateDomain(app.Name, null, domainSetup);
            var prcs = newDomain.CreateInstanceFromAndUnwrap(pathToDll, type) as KaomiProcess;

            Processes.Add(new ProcessID { ID = prcs.GetType().Name }, (newDomain, prcs));

            prcs.OnIteration();

            return new ProcessID { ID = prcs.GetType().Name };
        }

        public IEnumerable<string> ListProcesses()
        {
            return Processes.Keys.Select(pid => pid.ID);
        }

        public void UnloadProcess(string process)
        {
            var proc = Processes.FirstOrDefault(kvp => kvp.Key.ID.ToUpperInvariant().Contains(process.ToUpperInvariant()));

            if (proc.Key is null)
                return;

            AppDomain.Unload(proc.Value.appDomain);
            Processes.Remove(proc);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
