using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace PerfLogger
{

    class ResultDisposable : IDisposable
    {
        private readonly Action<long> result;
        private readonly Stopwatch watch;

        public  ResultDisposable(Action<long> outputFunction)
        {
            watch = new Stopwatch();
            watch.Start();
           result = outputFunction;
        }
        
        void System.IDisposable.Dispose()
        {
            result(watch.ElapsedMilliseconds);
        }
    }

    class PerfLogger 
    {
        private static ResultDisposable stoppuhr;
        public static  IDisposable Measure(Action<long> outputFunction )
        {
            stoppuhr = new ResultDisposable(outputFunction);
            return stoppuhr;
        }

        
       

    }

	class Program
	{

	    private static void Main(string[] args)
	    {

	        var sum = 0.0;
	        using (PerfLogger.Measure(t => Console.WriteLine("for: {0}", t)))
	            for (var i = 0; i < 100000000; i++) sum += i;
	        using (PerfLogger.Measure(t => Console.WriteLine("linq: {0}", t)))
	            sum -= Enumerable.Range(0, 100000000).Sum(i => (double) i);
	        Console.WriteLine(sum);
	       
	    }
	}
}
