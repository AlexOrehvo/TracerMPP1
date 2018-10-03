using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Tracer_mpp
{
    public class TraceResult
    {
        private readonly ConcurrentDictionary<int, Three> _threads;

        public TraceResult()
        {
            _threads = new ConcurrentDictionary<int, Three>();
        }
        
        public ConcurrentDictionary<int, Three> Threads
        {
            get { return _threads; }
        }

        public Three GetMethodThree(int index)
        {
            return _threads[index];
        }

        public void RegisterThread(int index)
        {
            _threads.TryAdd(index, new Three());
        }

        public List<Method> GetMethods(int threadId)
        {
            return GetMethodThree(threadId).GetRootMethod().getCalledMethod();
        }

        public long GetExecutionTime()
        {
            long time = 0;
            foreach (var thread in _threads)
            {
                foreach (var method in GetMethods(thread.Key))
                {
                    time += method.getTime().Milliseconds;
                }
            }
            return time;
        }
    }
}