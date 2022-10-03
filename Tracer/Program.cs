using System;
using TracerLib;
using System.Threading;
using System.IO;

namespace Tracer
{
    class Program
    {
        public TracerLib.Tracer Tracer;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Do();
        }

        public void Do()
        {
            Tracer = new TracerLib.Tracer();
            Method_1();
            Method_2();
            SaveToXml();
            SaveToJson();
            ShowResults();
            Console.ReadLine();
        }

        public void Method_1()
        {
            Tracer.StartTrace();
            InnerMethod_1_1();
            InnerMethod_1_2();
            InnerMethod_1_3();
            Tracer.StopTrace();
        }

        public void InnerMethod_1_1()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }
        public void InnerMethod_1_2()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }
        public void InnerMethod_1_3()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }

        public void Method_2()
        {
            Tracer.StartTrace();
            InnerMethod_2_1();
            Tracer.StopTrace();
        }

        public void InnerMethod_2_1()
        {
            Tracer.StartTrace();
            InnerMethod_2_1_1();
            Tracer.StopTrace();
        }

        public void InnerMethod_2_1_1()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }

        public void SaveToXml()
        {
            String xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\TraceResults\\XmlResults.xml");
            FileStream fs = new FileStream(xmlPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            TraceResult traceResult = Tracer.GetTraceResult();
            XmlSerializator xmlSerializator = new XmlSerializator();
            xmlSerializator.Serialize(sw, traceResult);
            xmlSerializator.Serialize(Console.Out, traceResult);

        }

        public void SaveToJson()
        {

        }
        public void ShowResults()
        {

        }
    }
}
