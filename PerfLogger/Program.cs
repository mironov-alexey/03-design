using System;
using System.Diagnostics;
using System.Linq;

namespace PerfLogging
{
	class Program
	{
		static void Main(string[] args)
		{
			var sum = 0.0;
			using (PerfLogger.Measure(t => Console.WriteLine("for: {0}", t)))
				for (var i = 0; i < 100000000; i++) sum += i;
			using (PerfLogger.Measure(t => Console.WriteLine("linq: {0}", t)))
				sum -= Enumerable.Range(0, 100000000).Sum(i => (double)i);
			Console.WriteLine(sum);
		}
	}

    public class PerfLogger: IDisposable
    {
        private readonly Action _printElapsed;
        private PerfLogger(Action printElapsed)
        {
            _printElapsed = printElapsed;
        }

        public static IDisposable Measure(Action<TimeSpan> logTime)
        {
            var sw = Stopwatch.StartNew();
            return new PerfLogger(() => logTime(sw.Elapsed));
        }

        public void Dispose()
        {
            _printElapsed();
        }
    }
}
