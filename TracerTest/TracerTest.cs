using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLib;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TracerTest
{
    [TestClass]
    public class TracerTest
    {
        Tracer tracer = new Tracer();
        void Method1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
        void Method2()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
        void InnerCall()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Method1();
            tracer.StopTrace();
        }
        void TwoThreadsCallOneMethod()
        {
            List<Thread> threads = new List<Thread>();
            for (int i=0; i<2; i++)
            {
                Thread thread = new Thread(Method1);
                threads.Add(thread);
                thread.Start();
            }
            foreach(Thread thread in threads)
            {
                thread.Join();
            }
        }
        [TestMethod]
        public void SingleMethodInMethodsTracer()
        {
            Method1();
            TraceResult traceResult = tracer.GetTraceResult();
            List<ThreadTracer> threadTracers = ConvertDictionaryToList(traceResult.ThreadTraces);
            Assert.AreEqual(1, threadTracers.Count);
            Assert.AreEqual(1, threadTracers[0].methodTracers.Count);
            Assert.AreEqual("Method1", threadTracers[0].methodTracers[0].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[0].methodTracers[0].ClassName);
        }

        [TestMethod]
        public void TwoMethodsInMethodTracer()
        {
            Method1();
            Method2();
            TraceResult traceResult = tracer.GetTraceResult();
            List<ThreadTracer> threadTracers = ConvertDictionaryToList(traceResult.ThreadTraces);

            Assert.AreEqual(1, threadTracers.Count);
            Assert.AreEqual(2, threadTracers[0].methodTracers.Count);
            Assert.AreEqual("Method1", threadTracers[0].methodTracers[0].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[0].methodTracers[0].ClassName);
            Assert.AreEqual("Method2", threadTracers[0].methodTracers[1].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[0].methodTracers[1].ClassName);

        }

        [TestMethod]
        public void SingleMethodWithInnerCall()
        {
            InnerCall();
            TraceResult traceResult = tracer.GetTraceResult();
            List<ThreadTracer> threadTracers = ConvertDictionaryToList(traceResult.ThreadTraces);
            Assert.AreEqual(1, threadTracers.Count);
            Assert.AreEqual(1, threadTracers[0].methodTracers.Count);
            Assert.AreEqual("InnerCall", threadTracers[0].methodTracers[0].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[0].methodTracers[0].ClassName);
            Assert.AreEqual(1, threadTracers[0].methodTracers[0].InnerMethods.Count);
            Assert.AreEqual("Method1", threadTracers[0].methodTracers[0].InnerMethods[0].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[0].methodTracers[0].InnerMethods[0].ClassName);
        }

        [TestMethod]
        public void TwoThreadsWithSingleMethod()
        {
            TwoThreadsCallOneMethod();
            TraceResult traceResult = tracer.GetTraceResult();
            List<ThreadTracer> threadTracers = ConvertDictionaryToList(traceResult.ThreadTraces);
            Assert.AreEqual(2, threadTracers.Count);
            Assert.AreEqual(1, threadTracers[0].methodTracers.Count);
            Assert.AreEqual(1, threadTracers[1].methodTracers.Count);
            Assert.AreEqual("Method1", threadTracers[0].methodTracers[0].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[0].methodTracers[0].ClassName);
            Assert.AreEqual("Method1", threadTracers[1].methodTracers[0].MethodName);
            Assert.AreEqual("TracerTest", threadTracers[1].methodTracers[0].ClassName);


        }

        private List<ThreadTracer> ConvertDictionaryToList(ConcurrentDictionary<int, ThreadTracer> cdThreadTracer)
        {
            List<ThreadTracer> threadTracers = new List<ThreadTracer>();
            foreach(ThreadTracer threadTracer in cdThreadTracer.Values)
            {
                threadTracers.Add(threadTracer);
            }
            return threadTracers;
        }
    }
}
