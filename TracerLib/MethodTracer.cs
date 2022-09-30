using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TracerLib
{
    public class MethodTracer
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public TimeSpan Time { get; set; }
        public List<MethodTracer> InnerMethods { get; set; }
        public Stopwatch StopWatch;

        public MethodTracer()
        {
            StackFrame sf = new StackFrame();
            MethodName = sf.GetMethod().Name;
            ClassName = sf.GetMethod().DeclaringType.Name;
            Time = new TimeSpan();
            InnerMethods = new List<MethodTracer>();
        }

        public void StartTrace()
        {
            StopWatch.Start();
        }

        public void StopTrace()
        {
            StopWatch.Stop();
            Time = StopWatch.Elapsed;
        }

    }
}
