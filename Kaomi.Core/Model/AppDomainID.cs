using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Core.Model
{
    [DataContract]
    public class AppDomainID : IEquatable<AppDomainID>
    {
        [DataMember]
        public string ID { get; set; }

        public override bool Equals(object obj) => ID.Equals(obj);

        public bool Equals(AppDomainID other) => this.ID.Equals(other.ID);

        public override int GetHashCode() => ID.GetHashCode();

        public override string ToString() => $"AppDomain[{ID}]";
    }
}
