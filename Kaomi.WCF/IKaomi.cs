using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kaomi.WCF
{
    [ServiceContract]
    public interface IKaomi
    {
        [OperationContract]
        FileInfo DownloadAssembly(string assemblyName, Uri path);

        [OperationContract]
        ProcessID InstanceProcess(FileInfo app, string type);

        [OperationContract]
        IEnumerable<string> ListProcesses();

        [OperationContract]
        void UnloadProcess(string process);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    // Puede agregar archivos XSD al proyecto. Después de compilar el proyecto, puede usar directamente los tipos de datos definidos aquí, con el espacio de nombres "Kaomi.WCF.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
