using System;
using System.Diagnostics;
using System.Threading;

namespace Tracer_mpp
{
    public class Tracer: ITracer
    {
        private static Tracer _instance;
        private static object _syncRoot = new object();
        private readonly TraceResult _traceResult;
        
        public Tracer() 
        {
            _traceResult = new TraceResult();
        }
        
        public void StartTrace()
        {
            Three three;

            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (!_traceResult.Threads.ContainsKey(threadId))
            {
                _traceResult.RegisterThread(threadId);
            }
            three = _traceResult.GetMethodThree(threadId);
            var stackTrace = new StackTrace();
            String name = stackTrace.GetFrame(1).GetMethod().Name;
            three.StartTrace(name);
        }

        public void StopTrace()
        {
            Three three = _traceResult.GetMethodThree(Thread.CurrentThread.ManagedThreadId);
            three.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public static Tracer GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new Tracer();
                    }
                }
            }

            return _instance;
        }
    }
}