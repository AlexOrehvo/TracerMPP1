using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tracer_mpp
{
    public class Method
    {
        private String _name;
        private Method _callingMethod;
        private List<Method> _calledMethods = new List<Method>();
        private Stopwatch _sw;
        private TimeSpan _time;
        
        public Method(String name, Method callingMethod)
        {
            _sw = new Stopwatch();
            _name = name;
            _callingMethod = callingMethod;
        }

        public String getName()
        {
            return _name;                
        }

        public List<Method> getCalledMethod()
        {
            return _calledMethods;
        }

        public Method getCallingMethod()
        {
            return _callingMethod;
        }

        public void StartTrace()
        {
            _sw.Start();
        }

        public void StopTrace()
        {
            
            _sw.Stop();
            _time = _sw.Elapsed;
        }

        public TimeSpan getTime()
        {
            return _time;
        }
    }
}