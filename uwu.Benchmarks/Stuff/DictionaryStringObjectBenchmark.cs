using System;
using System.Linq;
using System.Collections.Generic;

using Jil;
using Newtonsoft.Json;
using BenchmarkDotNet.Attributes;

namespace uwu.Benchmarks
{
	[MemoryDiagnoser]
	public class DictionaryStringObjectBenchmark
	{
		#region Fields

		private static readonly IDictionary<string, object> _commonTypes = new Dictionary<string, object>()
		{
			{ "string", "Yamete Kudasai" },
			{ "bool", false },
			{ "byte", byte.MaxValue },
			{ "sbyte", sbyte.MaxValue },
			{ "char", '\\' },
			{ "decimal", 1234.5M },
			{ "double", double.MaxValue },
			{ "float", float.MaxValue },
			{ "int", int.MaxValue },
			{ "uint", uint.MaxValue },
			{ "long", long.MaxValue },
			{ "ulong", ulong.MaxValue },
			{ "short", short.MaxValue },
			{ "ushort", ushort.MaxValue },
			{ "DateTime", DateTime.UtcNow },
			{ "TimeSpan", new TimeSpan(26, 52, 104) },
			{ "Guid", Guid.NewGuid() }
		};

		private static readonly IDictionary<string, object> _arraysOfCommonTypes = new Dictionary<string, object>()
		{
			{ "string[]", new[] { "dummy", null, "\\\\" } },
			{ "bool[]", new[] { true, false, true } },
			{ "byte[]", new[] { byte.MinValue, byte.MaxValue } },
			{ "sbyte[]", new[] { sbyte.MinValue, sbyte.MaxValue } },
			{ "char[]", new[] { '\b' , '\\', 'x' } },
			{ "decimal[]", new[] { 0.000000000123M, 1.12316454M } },
			{ "double[]", new[] { 0.00000001D, 1.12345D } },
			{ "float[]", new[] { 0.00002F, 1.123F } },
			{ "int[]", new[] { int.MinValue, int.MaxValue } },
			{ "uint[]", new[] { uint.MinValue, uint.MaxValue } },
			{ "long[]", new[] { long.MinValue, long.MaxValue } },
			{ "ulong[]", new[] { ulong.MinValue, ulong.MaxValue } },
			{ "short[]", new[] { short.MinValue, short.MaxValue } },
			{ "ushort[]", new[] { ushort.MinValue, ushort.MaxValue } },
			{ "DateTime[]", new[] { DateTime.MinValue.ToUniversalTime(), DateTime.MaxValue.ToUniversalTime() } },
			{ "TimeSpan[]", new[] { new TimeSpan(26, 52, 104), new TimeSpan(1) } },
			{ "Guid[]", new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() } },
			{ "object[]", new object[] { null, 45, "x", 0.01F } },
			{ "IDictionary", _commonTypes } //< Is not the same as bench against _commonTypes only..
		};

		private static readonly IDictionary<string, object> _mixed = Enumerable.Union(_commonTypes, _arraysOfCommonTypes).ToDictionary(x => x.Key, x => x.Value);

		#endregion

		[GlobalSetup]
		public void GlobalSetup()
		{
			Serialize_Dictionary_Of_Common_Types_System();
			Serialize_Dictionary_Of_Ienumerable_Types_System();
			Serialize_Dictionary_Of_Mixed_Values_System();

			Serialize_Dictionary_Of_Common_Types_Newtonsoft();
			Serialize_Dictionary_Of_Ienumerable_Types_Newtonsoft();
			Serialize_Dictionary_Of_Mixed_Values_Newtonsoft();

			Serialize_Dictionary_Of_Common_Types_Jil();
			Serialize_Dictionary_Of_Ienumerable_Types_Jil();
			Serialize_Dictionary_Of_Mixed_Values_Jil();

			Serialize_Dictionary_Of_Common_Types_Utf8Json();
			Serialize_Dictionary_Of_Ienumerable_Types_Utf8Json();
			Serialize_Dictionary_Of_Mixed_Values_Utf8Json();
		}

#pragma warning disable CA1822
		[Benchmark]
		public string Serialize_Dictionary_Of_Common_Types_System() => System.Text.Json.JsonSerializer.Serialize(_commonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Ienumerable_Types_System() => System.Text.Json.JsonSerializer.Serialize(_arraysOfCommonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Mixed_Values_System() => System.Text.Json.JsonSerializer.Serialize(_mixed);

		[Benchmark]
		public string Serialize_Dictionary_Of_Common_Types_Newtonsoft() => JsonConvert.SerializeObject(_commonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Ienumerable_Types_Newtonsoft() => JsonConvert.SerializeObject(_arraysOfCommonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Mixed_Values_Newtonsoft() => JsonConvert.SerializeObject(_mixed);

		[Benchmark]
		public string Serialize_Dictionary_Of_Common_Types_Jil() => JSON.SerializeDynamic(_commonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Ienumerable_Types_Jil() => JSON.SerializeDynamic(_arraysOfCommonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Mixed_Values_Jil() => JSON.SerializeDynamic(_mixed);

		[Benchmark]
		public string Serialize_Dictionary_Of_Common_Types_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_commonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Ienumerable_Types_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_arraysOfCommonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Mixed_Values_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_mixed);

		[Benchmark]
		public string Serialize_Dictionary_Of_Common_Types_Uwu() => Json.Serialize(_commonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Ienumerable_Types_Uwu() => Json.Serialize(_arraysOfCommonTypes);
		[Benchmark]
		public string Serialize_Dictionary_Of_Mixed_Values_Uwu() => Json.Serialize(_mixed);
#pragma warning restore CA1822
	}
}
