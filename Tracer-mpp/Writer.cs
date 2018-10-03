using System;
using System.Text;
using System.IO;

namespace Tracer_mpp
{
	class Writer: IWriter
	{
		public void Write(string data)
		{
			Console.WriteLine(data);
		}

		public void Write(string fileName, string data)
		{
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + fileName, data);
		}
	}
}
