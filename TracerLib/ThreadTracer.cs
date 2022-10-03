using System;
using System.Collections.Generic;

namespace TracerLib
{
    public class ThreadTracer
    {
        public int Id { get; set; }
        public TimeSpan Time;
        public List<MethodTracer> methodTracers;
        public Stack<MethodTracer> UnstoppedMethodTracers;

        public ThreadTracer(int id)
        {
            Id = id;
            Time = new TimeSpan();
            UnstoppedMethodTracers = new Stack <MethodTracer>();
            methodTracers = new List<MethodTracer>();
        }


        public void StartTrace()
        {
            MethodTracer methodTracer = new MethodTracer();
            if (UnstoppedMethodTracers.Count > 0)
            {
                MethodTracer lastUnstoppedMethodTracer = UnstoppedMethodTracers.Peek();
                lastUnstoppedMethodTracer.InnerMethods.Add(methodTracer);
            }
            UnstoppedMethodTracers.Push(methodTracer);
            methodTracer.StartTrace();
        }

        public void StopTrace()
        {
            MethodTracer lastUnstoppedMethodTracer = UnstoppedMethodTracers.Pop();
            lastUnstoppedMethodTracer.StopTrace();
            if (UnstoppedMethodTracers.Count == 0)
            {
                methodTracers.Add(lastUnstoppedMethodTracer);
                Time += lastUnstoppedMethodTracer.Time;

            }
        }

        

        
    }
}
