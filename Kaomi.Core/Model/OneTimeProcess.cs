using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Core.Model
{
    public abstract class OneTimeProcess : KaomiProcess
    {
        internal override bool OneTime() => true;
    }
}
