using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Core.Model
{
    [DataContract]
    public class ProcessID : IEquatable<ProcessID>
    {
        [DataMember]
        public string ID { get; set; }

        public override bool Equals(object obj) => ID.Equals(obj);

        public bool Equals(ProcessID other) => this.ID.Equals(other.ID);

        public override int GetHashCode() => ID.GetHashCode();

        public override string ToString() => $"Process[{ID}]";
    }
}
