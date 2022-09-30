using System;
using System.Linq;
using System.Collections.Concurrent;
namespace TracerLib
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadTracer> ThreadTraces;

        public TraceResult(ConcurrentDictionary<int, ThreadTracer> threadTraces)
        {
            ThreadTraces = threadTraces;
        }
        
    }
}
