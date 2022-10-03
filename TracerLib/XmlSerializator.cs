using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TracerLib
{
    public class XmlSerializator : ISerializator
    {
        public void Serialize(TextWriter textWriter, TraceResult traceResult)
        {
            XDocument xDocument = new XDocument(
                new XElement("root", from threadTracer in traceResult.ThreadTraces.Values select SaveThread(threadTracer)));

            XmlTextWriter xmlTextWriter = new XmlTextWriter(textWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            xDocument.WriteTo(xmlTextWriter);
        }
        
        public XElement SaveThread(ThreadTracer threadTracer)
        {
            return new XElement("thread",
                new XAttribute("id", threadTracer.Id),
                new XAttribute("time", threadTracer.Time.Milliseconds + "ms"),
                from methodTracer in threadTracer.methodTracers select SaveMethod(methodTracer));
        }

        public XElement SaveMethod(MethodTracer methodTracer)
        {
            XElement xElement = new XElement("method",
                new XAttribute("name", methodTracer.MethodName),
                new XAttribute("class", methodTracer.ClassName),
                new XAttribute("time", methodTracer.Time.Milliseconds + "ms"));
            if (methodTracer.InnerMethods.Count > 0)
            {
                xElement.Add(from innerMethod in methodTracer.InnerMethods select SaveMethod(innerMethod));
            }
            return xElement;
        }
    }
}
