using System.Threading;

namespace Tracer_mpp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestObj test = new TestObj();
           
            Thread thread = new Thread(test.foo);
            thread.Start();
            test.foo();
            thread.Join();

			ISerializer serializer = new JsonTraceResultSerializer();
			IWriter writer = new Writer();
			writer.Write("trace-result.json", serializer.Serialize(test.getThree()));
        }
        
       
    }
}