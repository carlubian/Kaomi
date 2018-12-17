using Kaomi.WCF.Logic;
using Kaomi.WCF.Model;
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
        private IDictionary<AppDomainID, Assembly> AppDomains = new LenientDictionary<AppDomainID, Assembly>();

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
            }

            return new FileInfo($"{assemblyName}.dll");
        }

        /// <summary>
        /// Carga el ensamblado en un nuevo AppDomain (mentira de momento).
        /// </summary>
        /// <param name="path">Ruta al ensamblado</param>
        /// <returns>ID del AppDomain</returns>
        public AppDomainID CreateAppDomain(FileInfo path)
        {
            var assembly = Assembly.LoadFile(path.FullName);
            var id = new AppDomainID { ID = assembly.GetName().Name };
            AppDomains.Add(id, assembly);

            return id;
        }

        /// <summary>
        /// Devuelve una lista de los AppDomains cargados actualmente.
        /// </summary>
        /// <returns>Lista de AppDomains (no exactamente)</returns>
        public IEnumerable<string> ListAppDomains()
        {
            foreach (var kvp in AppDomains)
                yield return $"{kvp.Key}: {kvp.Value.FullName}";
        }

        public void UnloadAppDomain(string id) => throw new NotImplementedException();

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
