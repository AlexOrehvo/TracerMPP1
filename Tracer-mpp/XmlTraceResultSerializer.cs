using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Tracer_mpp
{
    public class XmlTraceResultSerializer: ISerializer
    {
        
        public string Serialize(TraceResult traceResult)
        {
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			XmlTextWriter xWriter = new XmlTextWriter(sw);
            xWriter.Formatting = Formatting.Indented;
            
            xWriter.WriteStartElement("Threads");
            foreach (var methodThree in traceResult.Threads)
            {
                xWriter.WriteStartElement("Thread");
                xWriter.WriteStartAttribute("id");
                xWriter.WriteString(methodThree.Key + "");
                xWriter.WriteEndAttribute();
                for (int j = 0; j < traceResult.GetMethods(methodThree.Key).Count; j++)
                {
                    WriteMethodInfo(xWriter, traceResult.GetMethods(methodThree.Key)[j]);
                }
                xWriter.WriteEndElement();
            }
            xWriter.WriteEndElement();
            xWriter.Close();

			return sw.ToString();
        }

        private void WriteMethodInfo(XmlTextWriter xWriter, Method method)
        {
            xWriter.WriteStartElement("name");
            xWriter.WriteString(method.getName());
            xWriter.WriteEndElement();
            xWriter.WriteStartElement("time");
            xWriter.WriteString(method.getTime().ToString());
            xWriter.WriteEndElement();
            xWriter.WriteStartElement("Methods");
            for (int i = 0; i < method.getCalledMethod().Count; i++)
            {
                WriteMethodInfo(xWriter, method.getCalledMethod()[i]);
            }
            xWriter.WriteEndElement();
        }
    }
}