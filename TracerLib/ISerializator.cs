using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TracerLib
{
    public interface ISerializator
    {
        void Serialize(TextWriter textWriter,TraceResult traceResult);
    }
}
