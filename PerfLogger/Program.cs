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
        private readonly Action<long> time;
        private readonly Stopwatch watch;

        public  ResultDisposable(Stopwatch inWatch, Action<long> inTime)
        {
            watch = new Stopwatch();
            watch.Start();
            time = inTime;
        }
        
        void System.IDisposable.Dispose()
        {
            time(watch.ElapsedMilliseconds);
        }
    }

    class PerfLogger 
    {
        private static Stopwatch watch;
        private static ResultDisposable help;
        public static  IDisposable Measure(Action<long> time )
        {
            help = new ResultDisposable(watch,time);
            return help;
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
