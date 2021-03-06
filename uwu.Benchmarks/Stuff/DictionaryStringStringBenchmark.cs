using System.Collections.Generic;

using Jil;
using Newtonsoft.Json;
using BenchmarkDotNet.Attributes;

namespace uwu.Benchmarks
{
	[MemoryDiagnoser]
	public class DictionaryStringStringBenchmark
	{
		#region Fields

		private static readonly IDictionary<string, string> _small = new Dictionary<string, string>()
		{
			{ "Dummy Key", "Dummy Value" },
			{ "Key", "Value" },
			{ "K", "V" }
		};

		private static readonly IDictionary<string, string> _medium = new Dictionary<string, string>()
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

		private static readonly IDictionary<string, string> _large = new Dictionary<string, string>()
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

		[GlobalSetup]
		public void GlobalSetup()
		{
			Serialize_Small_Dict_System();
			Serialize_Medium_Dict_System();
			Serialize_Large_Dict_System();

			Serialize_Small_Dict_Newtonsoft();
			Serialize_Medium_Dict_Newtonsoft();
			Serialize_Large_Dict_Newtonsoft();

			Serialize_Small_Dict_Jil();
			Serialize_Medium_Dict_Jil();
			Serialize_Large_Dict_Jil();

			Serialize_Small_Dict_Utf8Json();
			Serialize_Medium_Dict_Utf8Json();
			Serialize_Large_Dict_Utf8Json();
		}

#pragma warning disable CA1822
		[Benchmark]
		public string Serialize_Small_Dict_System() => System.Text.Json.JsonSerializer.Serialize(_small);
		[Benchmark]
		public string Serialize_Medium_Dict_System() => System.Text.Json.JsonSerializer.Serialize(_medium);
		[Benchmark]
		public string Serialize_Large_Dict_System() => System.Text.Json.JsonSerializer.Serialize(_large);

		[Benchmark]
		public string Serialize_Small_Dict_Newtonsoft() => JsonConvert.SerializeObject(_small);
		[Benchmark]
		public string Serialize_Medium_Dict_Newtonsoft() => JsonConvert.SerializeObject(_medium);
		[Benchmark]
		public string Serialize_Large_Dict_Newtonsoft() => JsonConvert.SerializeObject(_large);

		[Benchmark]
		public string Serialize_Small_Dict_Jil() => JSON.Serialize(_small);
		[Benchmark]
		public string Serialize_Medium_Dict_Jil() => JSON.Serialize(_medium);
		[Benchmark]
		public string Serialize_Large_Dict_Jil() => JSON.Serialize(_large);

		[Benchmark]
		public string Serialize_Small_Dict_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_small);
		[Benchmark]
		public string Serialize_Medium_Dict_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_medium);
		[Benchmark]
		public string Serialize_Large_Dict_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_large);

		[Benchmark]
		public string Serialize_Small_Dict_Uwu() => Json.Serialize(_small);
		[Benchmark]
		public string Serialize_Medium_Dict_Uwu() => Json.Serialize(_medium);
		[Benchmark]
		public string Serialize_Large_Dict_Uwu() => Json.Serialize(_large);

		[Benchmark]
		public string SerializeUnsafe_Small_Dict_Uwu() => Json.SerializeUnsafe(_small);
		[Benchmark]
		public string SerializeUnsafe_Medium_Dict_Uwu() => Json.SerializeUnsafe(_medium);
		[Benchmark]
		public string SerializeUnsafe_Large_Dict_Uwu() => Json.SerializeUnsafe(_large);

		[Benchmark]
		public string SerializeWithoutNullOrEmpty_Small_Dict_Uwu() => Json.SerializeWithoutNullOrEmpty(_small);
		[Benchmark]
		public string SerializeWithoutNullOrEmpty_Medium_Dict_Uwu() => Json.SerializeWithoutNullOrEmpty(_medium);
		[Benchmark]
		public string SerializeWithoutNullOrEmpty_Large_Dict_Uwu() => Json.SerializeWithoutNullOrEmpty(_large);
#pragma warning restore CA1822
	}
}
