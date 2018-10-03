using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Tracer_mpp;

namespace TracerTests
{
    
    public class TracerTests
    {
        private Tracer _tracer = Tracer.GetInstance();
        private const int _waitTime = 100;
        private const int _numberOfThread = 4;
        
        private void TimeTest(long actual, long expected)
        {
            Console.WriteLine(actual + " - " + expected);
            Assert.IsTrue(actual >= expected);
        }

        private void foo()
        {
            _tracer.StartTrace();
            Thread.Sleep(_waitTime);
            bar();
            _tracer.StopTrace();
        }

        private void bar()
        {
            _tracer.StartTrace();
            Thread.Sleep(_waitTime);
            _tracer.StopTrace();
        }

        [Test]
        public void SingleThreadTest()
        {
            foo();
            TimeTest(_tracer.GetTraceResult().GetMethods(Thread.CurrentThread.ManagedThreadId)[0].getTime().Milliseconds, _waitTime*2);
        }

        [Test]
        public void MultiThreadTest()
        {
            long expected = 0;
            var threads = new List<Thread>();
            for (int i = 0; i < _numberOfThread; i++)
            {    
                Thread thread = new Thread(foo);
                threads.Add(thread);
                thread.Start();
                expected += _waitTime*2;
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            } 
            
            TimeTest(_tracer.GetTraceResult().GetExecutionTime(), expected);
        }
    }
}