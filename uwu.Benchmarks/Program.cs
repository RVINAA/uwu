using System;

using BenchmarkDotNet.Running;

namespace uwu.Benchmarks
{
	public class Program
	{
		public static void Main()
		{
#if false
			BenchmarkRunner.Run<DictionaryStringStringBenchmark>();
#elif true
			BenchmarkRunner.Run<DictionaryStringObjectBenchmark>();
#endif
			Console.ReadKey();
		}
	}
}
