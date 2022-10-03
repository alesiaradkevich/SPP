using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TracerLib
{
    public interface ISerializer
    {
        void Serialize(TextWriter textWriter,TraceResult traceResult);
    }
}
