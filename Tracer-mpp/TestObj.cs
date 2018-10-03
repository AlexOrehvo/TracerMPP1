using System;
using System.Threading;

namespace Tracer_mpp
{
    public class TestObj
    {
        private readonly Tracer _tracer = Tracer.GetInstance();

        public void foo()
        {
            _tracer.StartTrace();
            Thread.Sleep(new TimeSpan(0,0,1));
            bar();
            bar2();
            _tracer.StopTrace();
        }

        public void bar()
        {
            _tracer.StartTrace();
            Thread.Sleep(new TimeSpan(0,0,1));
            _tracer.StopTrace();
        }

        public TraceResult getThree()
        {
            return _tracer.GetTraceResult();
        }

        public void bar2()
        {
            _tracer.StartTrace();
            Thread.Sleep(new TimeSpan(0,0,1));
            _tracer.StopTrace();
        }
    }
}