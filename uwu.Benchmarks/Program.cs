using System;

using BenchmarkDotNet.Running;

using uwu.Benchmarks.Stuff;

namespace uwu.Benchmarks
{
	public class Program
	{
		public static void Main()
		{
			BenchmarkRunner.Run<DictionaryBench>();
			Console.ReadKey();
		}
	}
}
