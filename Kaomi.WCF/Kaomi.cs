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
    public class Kaomi : IKaomi
    {
        private IDictionary<ProcessID, object> Processes = new LenientDictionary<ProcessID, object>();

        /// <summary>
        /// Descarga un ensamblado desde una URI.
        /// </summary>
        /// <param name="assemblyName">Nombre del ensamblado</param>
        /// <param name="path">URI donde se encuentra el ensamblado</param>
        /// <returns>Ruta del ensamblado</returns>
        public FileInfo DownloadAssembly(string assemblyName, Uri path)
        {
            if (!IsFile.BeingUsed(new FileInfo($"{assemblyName}.dll")))
                using (var web = new WebClient())
                    web.DownloadFile(path, $"{assemblyName}.dll");
            else
            {
                // TODO Descargar AppDomain de memoria y borrar el archivo
                ;
            }

            return new FileInfo($"{assemblyName}.dll");
        }

        /// <summary>
        /// Instancia un proceso contenido en el AppDomain indicado.
        /// </summary>
        /// <param name="app">ID del AppDomain</param>
        /// <param name="type">Nombre del tipo</param>
        /// <returns></returns>
        public ProcessID InstanceProcess(FileInfo app, string type)
        {
            var asm = Assembly.LoadFile(app.FullName);
            var proc = asm.CreateInstance(type) as KaomiProcess;
            proc.OnIteration();

            //TODO
            proc.OnIteration();

            return new ProcessID { ID = proc.GetType().Name };
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
