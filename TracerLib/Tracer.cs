using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;


namespace TracerLib
{
    public class Tracer : ITracer
    {

        public TraceResult traceResult;

        public Tracer()
        {
            traceResult = new TraceResult(new ConcurrentDictionary<int, ThreadTracer>());
        }
        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public void StartTrace()
        {
            ThreadTracer curThreadTrace = GetThreadTrace(Thread.CurrentThread.ManagedThreadId);
            curThreadTrace.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer currthreadTracer = GetThreadTrace(Thread.CurrentThread.ManagedThreadId);
            currthreadTracer.StopTrace();
        }

        public ThreadTracer GetThreadTrace(int id)
        {
            return traceResult.ThreadTraces.GetOrAdd(id, new ThreadTracer(id));
        }
    }
}
       
