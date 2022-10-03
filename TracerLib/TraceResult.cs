using System;
using System.Linq;
using System.Collections.Concurrent;
namespace TracerLib
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadTracer> ThreadTraces;
        //public int Id;
        public TimeSpan Time;

        public TraceResult(ConcurrentDictionary<int, ThreadTracer> threadTraces)
        {
            ThreadTraces = threadTraces;
            
            Time = new TimeSpan();
        }

        
    }
}
