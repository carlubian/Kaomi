using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.WCF.Model
{
    [DataContract]
    public class AppDomainID
    {
        [DataMember]
        public string ID { get; set; }

        public override bool Equals(object obj) => ID.Equals(obj);

        public override int GetHashCode() => ID.GetHashCode();

        public override string ToString() => $"AppDomain[{ID}]";
    }
}
