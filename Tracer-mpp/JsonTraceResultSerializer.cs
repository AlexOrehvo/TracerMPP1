using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Tracer_mpp
{
    public class JsonTraceResultSerializer: ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

			JsonWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Formatting.Indented;
            
            writer.WriteStartObject();
            writer.WritePropertyName("threads");
            writer.WriteStartArray();
            foreach (var methodThree in traceResult.Threads)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("id");
                writer.WriteValue(methodThree.Key);
                writer.WritePropertyName("methods");
                writer.WriteStartArray();
                for (int j = 0; j < traceResult.GetMethods(methodThree.Key).Count; j++)
                {
                    WriteMethodInfo(writer, traceResult.GetMethods(methodThree.Key)[j]);
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();

			return sw.ToString();
            //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory+"result.json", sw.ToString());
        }
        
        private void WriteMethodInfo(JsonWriter writer, Method method)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("name");
            writer.WriteValue(method.getName());
            writer.WritePropertyName("time");
            writer.WriteValue(method.getTime());
            writer.WritePropertyName("methods");
            writer.WriteStartArray();
            for (int i = 0; i < method.getCalledMethod().Count; i++)
            {
                WriteMethodInfo(writer, method.getCalledMethod()[i]);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}