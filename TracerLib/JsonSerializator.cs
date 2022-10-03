using System;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TracerLib
{
    public class JsonSerializator : ISerializator
    {
        public void Serialize(TextWriter textWriter, TraceResult traceResult)
        {
            var jToken = from threadTracer in traceResult.ThreadTraces.Values
                          select SerializeThread(threadTracer);

            JObject jResult = new JObject
            {
                {"thread", new JArray(jToken) }
            };

            JsonTextWriter jsonTextWriter = new JsonTextWriter(textWriter);
            jsonTextWriter.Formatting = Formatting.Indented;
            jResult.WriteTo(jsonTextWriter);
            jsonTextWriter.Flush();
        }

        private JToken SerializeThread(ThreadTracer threadTracer)
        {
            var jMethods = from methodTracer in threadTracer.methodTracers select SerializeMethod(methodTracer);
            return new JObject
            {
                {"id", threadTracer.Id },
                {"time", threadTracer.Time.Milliseconds+"ms" },
                {"methods", new JArray(jMethods) }
            };

        }

        private JToken SerializeMethod(MethodTracer methodTracer)
        {
            var jMethods = new JObject
            {
                {"name", methodTracer.MethodName },
                {"class", methodTracer.ClassName },
                {"time", methodTracer.Time.Milliseconds+"ms" }
            };
            if (methodTracer.InnerMethods.Count > 0)
            {
                jMethods.Add("methods", new JArray(from innerMethods in methodTracer.InnerMethods 
                                                   select SerializeMethod(innerMethods)));
            }
            return jMethods;
        }
    }
}
