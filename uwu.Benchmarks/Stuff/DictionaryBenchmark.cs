using System;
using System.Collections.Generic;

using Jil;
using Newtonsoft.Json;
using BenchmarkDotNet.Attributes;

namespace uwu.Benchmarks.Stuff
{
	[MemoryDiagnoser]
	public class DictionaryBenchmark
	{
#pragma warning disable CA1822
#if true

		#region IDictionary<string, string> stuff

		#region Fields

		private static readonly IDictionary<string, string> _dictionaryStr = new Dictionary<string, string>()
		{
			{ "Dummy Key", "Dummy Value" },
			{ "Key", "Value" },
			{ "K", "V" }
		};

		private static readonly IDictionary<string, string> _dictionaryStrOne = new Dictionary<string, string>()
		{
			{ "01", "Hiya, Barbie\n" },
			{ "02", "Hi, Ken\n" },
			{ "03", "You want to go for a ride?\n" },
			{ "04", "Sure, Ken\n" },
			{ "05", "Jump in\n" },
			{ "06", "\n" },
			{ "07", "I'm a Barbie girl, in the Barbie world\n" },
			{ "08", "Life in plastic, it's fantastic\n" },
			{ "09", "You can brush my hair, undress me everywhere\n" },
			{ "11", "Imagination, life is your creation\n" },
			{ "12", "\n" },
			{ "13", "Come on, Barbie, let's go party\n" },
			{ "14", "\n" },
			{ "15", "I'm a Barbie girl, in the Barbie world\n" },
			{ "16", "Life in plastic, it's fantastic\n" },
			{ "17", "You can brush my hair, undress me everywhere\n" },
			{ "18", "Imagination, life is your creation\n" },
			{ "19", "\n" },
			{ "20", "I'm a blond bimbo girl in a fantasy world\n" },
			{ "21", "Dress me up, make it tight, I'm your dolly\n" },
			{ "22", "You're my doll, rock'n'roll, feel the glamour in pink\n" },
			{ "23", "Kiss me here, touch me there, hanky panky\n" }
		};

		private static readonly IDictionary<string, string> _dictionaryStrTwo = new Dictionary<string, string>()
		{
			{ "01", "Hiya, Barbie\n" },
			{ "02", "H\fi, Ken\n" },
			{ "03", "You want to go for a ride?\n" },
			{ "04", "S\rure, Ken\n" },
			{ "05", "Jump in\n" },
			{ "06", "\n" },
			{ "07", "I'm a Ba\brbie girl, \bin the B\tar\rbie world\n" },
			{ "0\b8", "Lif\re in plastic, it's fantastic\n" },
			{ "09", "Y\tou \fcan brush\b my hair, \bundress me everywhere\n" },
			{ "11", "Imaginati\fon, life is your creation\n" },
			{ "\r\r12", "\n" },
			{ "13", "Come on, B\rarb\tie, let's g\bo party\n" },
			{ "14", "\n" },
			{ "1\f5", "I'm a Ba\frbie girl, in th\be Ba\rrbie world\n" },
			{ "16", "Li\\t in pl\bast\ric, it\r's fantastic\n" },
			{ "1\f7", "You can brush\r my hair, u\t\bndress me everywhere\n" },
			{ "18", "Imagi\fnat\\t, li\tfe i\bs your creation\n" },
			{ "1\f9", "\n" },
			{ "2\f0", "I'm a blon\rd bimbo gir\tl in a fan\rtasy world\n" },
			{ "21", "Dr\res\bs m\te up,\t mak\re it ti\bght, I'm your dolly\n" },
			{ "2\f2", "You're my d\ro\rll, ro\rck'n'roll, feel the glamour in pink\n" },
			{ "23", "Kiss \bme\t here, to\t\rch me \t\t, hanky panky\n" },
			{ "24", "Hiya, Ba\trbie\n" },
			{ "2\f5", "Hi, Ke\tn\n" },
			{ "26", "You w\fant to go \tor a ride?\n" },
			{ "2\b7", "Su\tr\be, Ken\n" },
			{ "28", "J\tump in\n" },
			{ "29", "\n" },
			{ "3\r0", "I'm a Ba\trbie g\tirl, in t\bhe Barbie world\n" },
			{ "3\r1", "Life \tin pla\\rbstic, it's fant\rastic\n" },
			{ "3\r2", "Y\fou can b\rrush my hair, undres\rs me everywhere\n" },
			{ "3\b3", "Im\ra\tgina\ftion, life i\bs your creation\n" },
			{ "34", "\n" },
			{ "35", "Come\b on, Barbie, let's go party\n" },
			{ "36", "\n" },
			{ "37", "I'm a \fBarbie girl, in t\rhe\t Bar\rbie \tworld\n" },
			{ "\b38", "Li\tfe i\fn plas\ttic, \bit's fan\rtastic\n" },
			{ "39", "Y\tou can bru\tsh my hair, und\bress \tme everywhere\n" },
			{ "40", "Imag\tin\tation, \blife i\rs your cr\reation\n" },
			{ "4\f1", "\n" },
			{ "42", "I'm \ta b\tlond\f bim\rbo girl in \fa fa\tntasy world\n" },
			{ "43", "Dres\b \tme up, make it tight, I'm your dolly\n" },
			{ "44", "You're m\ty \f\f\fdoll, r\bock'n'\troll, f\feel the glamour in pink\n" },
			{ "45", "Ki\bss me\t here, to\ruch me \rthere, hank\by panky\n" }
		};

		#endregion

		[Benchmark]
		public string SerializeDictionaryStr_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryStr);

		[Benchmark]
		public string SerializeDictionaryStr_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryStr);

		[Benchmark]
		public string SerializeDictionaryStr_Jil() => JSON.Serialize(_dictionaryStr);

		[Benchmark]
		public string SerializeDictionaryStr_Uwu() => Json.Serialize(_dictionaryStr);

		[Benchmark]
		public string SerializeDictionaryStrOne_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryStrOne);

		[Benchmark]
		public string SerializeDictionaryStrOne_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryStrOne);

		[Benchmark]
		public string SerializeDictionaryStrOne_Jil() => JSON.Serialize(_dictionaryStrOne);

		[Benchmark]
		public string SerializeDictionaryStrOne_Uwu() => Json.Serialize(_dictionaryStrOne);

		[Benchmark]
		public string SerializeDictionaryStrTwo_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryStrTwo);

		[Benchmark]
		public string SerializeDictionaryStrTwo_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryStrTwo);

		[Benchmark]
		public string SerializeDictionaryStrTwo_Jil() => JSON.Serialize(_dictionaryStrTwo);

		[Benchmark]
		public string SerializeDictionaryStrTwo_Uwu() => Json.Serialize(_dictionaryStrTwo);

		#endregion

		#region IDictionary<string, object> stuff

		#region Fields

		private static readonly IDictionary<string, object> _dictionaryObj = new Dictionary<string, object>()
		{
			{ "string", "string" },
			{ "bool", false },
			{ "byte", byte.MaxValue },
			{ "sbyte", sbyte.MaxValue },
			{ "char", '\\' },
			{ "decimal", decimal.MaxValue },
			{ "double", double.MaxValue },
			{ "float", float.MaxValue },
			{ "int", int.MaxValue },
			{ "uint", uint.MaxValue },
			{ "long", long.MaxValue },
			{ "ulong", ulong.MaxValue },
			{ "short", short.MaxValue },
			{ "ushort", ushort.MaxValue },
			{ "DateTime", DateTime.UtcNow },
			{ "TimeSpan", TimeSpan.MaxValue },
			{ "Null", null }
		};

		private static readonly IDictionary<string, object> _dictionaryObjOne = new Dictionary<string, object>()
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
			{ "object[]", new object[] { null, 45, "x", 0.01F } }
		};

		#endregion

		[Benchmark]
		public string SerializeDictionaryObj_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryObj);

		[Benchmark]
		public string SerializeDictionaryObj_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryObj);

		[Benchmark]
		public string SerializeDictionaryObj_Uwu() => Json.Serialize(_dictionaryObj);

		[Benchmark]
		public string SerializeDictionaryObjOne_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryObjOne);

		[Benchmark]
		public string SerializeDictionaryObjOne_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryObjOne);

		[Benchmark]
		public string SerializeDictionaryObjOne_Uwu() => Json.Serialize(_dictionaryObjOne);

		#endregion

#elif true

		// ...

#endif
#pragma warning restore CA1822
	}
}
