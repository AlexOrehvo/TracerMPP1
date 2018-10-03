using System;

namespace Tracer_mpp
{
    public class Three
    {
        private Method _root;
        private Method _parent;
        private Method _current;

        public Three()
        {
            _root = new Method("111", null);
            _parent = _root;
        }

        public void StartTrace(String name)
        {
            _current = new Method(name, _parent);
            _parent.getCalledMethod().Add(_current);
            _parent = _current;
            _current.StartTrace();
        }

        public void StopTrace()
        {
            _current = _parent;
            _current.StopTrace();
            _parent = _parent.getCallingMethod();
        }

        public Method GetRootMethod()
        {
            return _root;
        }
    }
}