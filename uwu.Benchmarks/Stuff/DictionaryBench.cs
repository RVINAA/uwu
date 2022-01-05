using System.Collections.Generic;

using Jil;
using Newtonsoft.Json;
using BenchmarkDotNet.Attributes;

namespace uwu.Benchmarks.Stuff
{
	public class DictionaryBench
	{
		#region Fields

		private static readonly IDictionary<string, string> _dictionary = new Dictionary<string, string>()
		{
			{ "Dummy Key", "Dummy Value" },
			{ "Key", "Value" },
			{ "K", "V" }
		};

		private static readonly IDictionary<string, string> _dictionaryOne = new Dictionary<string, string>()
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

		private static readonly IDictionary<string, string> _dictionaryTwo = new Dictionary<string, string>()
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
		public string SerializeDictionary_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionary);

		[Benchmark]
		public string SerializeDictionary_Newtonsoft() => JsonConvert.SerializeObject(_dictionary);

		[Benchmark]
		public string SerializeDictionary_Jil() => JSON.Serialize(_dictionary);

		[Benchmark]
		public string SerializeDictionary_Uwu() => Json.Serialize(_dictionary);

		[Benchmark]
		public string SerializeDictionaryOne_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryOne);

		[Benchmark]
		public string SerializeDictionaryOne_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryOne);

		[Benchmark]
		public string SerializeDictionaryOne_Jil() => JSON.Serialize(_dictionaryOne);

		[Benchmark]
		public string SerializeDictionaryOne_Uwu() => Json.Serialize(_dictionaryOne);

		[Benchmark]
		public string SerializeDictionaryTwo_Utf8Json() => Utf8Json.JsonSerializer.ToJsonString(_dictionaryTwo);

		[Benchmark]
		public string SerializeDictionaryTwo_Newtonsoft() => JsonConvert.SerializeObject(_dictionaryTwo);

		[Benchmark]
		public string SerializeDictionaryTwo_Jil() => JSON.Serialize(_dictionaryTwo);

		[Benchmark]
		public string SerializeDictionaryTwo_Uwu() => Json.Serialize(_dictionaryTwo);
	}
}
