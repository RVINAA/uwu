using System;
using System.Linq;
using System.Collections.Generic;

using Jil;
using Newtonsoft.Json;
using NUnit.Framework;

namespace uwu.Tests
{
	public class DictionaryTests
	{
		#region Fields

		private static readonly IDictionary<string, string> _strStr = new Dictionary<string, string>();
		private static readonly IDictionary<string, object> _strObj = new Dictionary<string, object>();

		// NOTE: In order to check that, by default.. I'm doing the same as other serializers.
		private static readonly IDictionary<string, Func<IDictionary<string, string>, string>> _serializersStr = new Dictionary<string, Func<IDictionary<string, string>, string>>()
		{
			{ nameof(Utf8Json), x => Utf8Json.JsonSerializer.ToJsonString(x) },
			{ nameof(Newtonsoft), x => JsonConvert.SerializeObject(x) },
			{ nameof(Jil), x => JSON.Serialize(x) },
			{ nameof(uwu), x => Json.Serialize(x) },
			{ "owo", x => Json.SerializeUnsafe(x) }
		};

		// NOTE: In order to check that, by default.. I'm doing the same as other serializers.
		private static readonly IDictionary<string, Func<IDictionary<string, object>, string>> _serializersObj = new Dictionary<string, Func<IDictionary<string, object>, string>>()
		{
			{ nameof(Utf8Json), x => Utf8Json.JsonSerializer.ToJsonString(x) },
			{ nameof(Newtonsoft), x => JsonConvert.SerializeObject(x) },
			{ nameof(Jil), x => JSON.Serialize(x) }, //< SerializeDynamic<>?
			{ nameof(uwu), x => Json.Serialize(x) }
		};

		private static readonly object[] _strStrItems = new[]
		{
			new object[]
			{
				"{\"A\":\"B\",\"C\":\"\",\"D\":null,\"E\":\"\\\"\",\"F\":\"\\\\\",\"G\":\"\\b\",\"H\":\"\\t\",\"I\":\"\\n\",\"J\":\"\\f\",\"K\":\"\\r\"}",
				new Dictionary<string, string>()
				{
					{ "A", "B" }, { "C", "" }, { "D", null }, { "E", "\"" }, { "F", "\\" }, { "G", "\b" }, { "H", "\t" }, { "I", "\n" }, { "J", "\f" }, { "K", "\r" }
				}
			},
			new object[] { "{\"A\":\"B\",\"C\":null,\"D\":\"E\"}", new Dictionary<string, string>() { { "A", "B" }, { "C", null }, { "D", "E" } } },
			new object[] { "{\"A\":null,\"B\":\"C\",\"D\":null}", new Dictionary<string, string>() { { "A", null }, { "B", "C" }, { "D", null } } },
			new object[] { "{\"A\":null,\"B\":null,\"C\":null}", new Dictionary<string, string>() { { "A", null }, { "B", null }, { "C", null } } },
			new object[] { "{\"\\\\z\\\"\\b\\\"zzz\":\"z\\t\\n\"}", new Dictionary<string, string>() { { "\\z\"\b\"zzz", "z\t\n" } } }
		};

		private static readonly object[] _strObjItems = new[]
		{
			new object[] //< Value & Common types supported..
			{
				"{\"string\":\"\\\\\\b\",\"bool\":false,\"byte\":255,\"sbyte\":127,\"char\":\"\\\\\",\"decimal\":1234.5,\"double\":1.7976931348623157E+308,\"float\":3.4028235E+38,\"int\":2147483647,\"uint\":4294967295,\"long\":9223372036854775807,\"ulong\":18446744073709551615,\"short\":32767,\"ushort\":65535,\"DateTime\":\"9999-12-31T22:59:59.9999999Z\",\"TimeSpan\":\"1.02:53:44\",\"Guid\":\"e045b922-5a28-42ae-899c-4343223345c5\"}",
				new Dictionary<string, object>()
				{
					{ "string", "\\\b" },
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
					{ "DateTime", DateTime.MaxValue.ToUniversalTime() },
					{ "TimeSpan", new TimeSpan(26, 52, 104) },
					{ "Guid", new Guid("e045b922-5a28-42ae-899c-4343223345c5") }
				},
				new[] { nameof(Jil) }
			},
			new object[] //< Some stuff against arrays.. (of supported common & value types).
			{
				"{\"string[]\":[\"dummy\",null,\"\\\\\\\\\"],\"bool[]\":[true,false,true],\"byte[]\":[0,255],\"sbyte[]\":[-128,127],\"char[]\":[\"\\b\",\"\\\\\",\"x\"],\"decimal[]\":[0.000000000123,1.12316454],\"double[]\":[1E-08,1.12345],\"float[]\":[2E-05,1.123],\"int[]\":[-2147483648,2147483647],\"uint[]\":[0,4294967295],\"long[]\":[-9223372036854775808,9223372036854775807],\"ulong[]\":[0,18446744073709551615],\"short[]\":[-32768,32767],\"ushort[]\":[0,65535],\"DateTime[]\":[\"0001-01-01T00:00:00.0000000Z\",\"9999-12-31T22:59:59.9999999Z\"],\"TimeSpan[]\":[\"1.02:53:44\",\"00:00:00.0000001\"],\"Guid[]\":[\"e045b922-5a28-42ae-899c-4343223345c5\"],\"object[]\":[null,45,\"x\",0.01]}",
				new Dictionary<string, object>()
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
					{ "Guid[]", new[] { new Guid("e045b922-5a28-42ae-899c-4343223345c5") } },
					{ "object[]", new object[] { null, 45, "x", 0.01F } }
				},
				new[] { nameof(Utf8Json), nameof(Newtonsoft), nameof(Jil) }
			},
			new object[] //< Multidimensional arrays not supported; but array of arrays yesss.
			{
				"{\"string[][]\":[[\"\\\"\",null],[null,\"dummy\"]],\"int[][][]\":[[[1,2],[3,4]]],\"object[][]\":[1,[\"x\",\"y\"],[1.1,[\"\\\\\"]]]}",
				new Dictionary<string, object>()
				{
					{ "string[][]", new[] { new[] { "\"", null }, new[] { null, "dummy" } } },
					{ "int[][][]", new[] { new[] { new[] { 1, 2 }, new[] { 3, 4 } } } },
					{ "object[][]", new object[] { 1, new[] { "x", "y" }, new object[] { 1.1F, new[] { '\\' } } } }
				},
				new[] { nameof(Utf8Json), nameof(Jil) }
			},
			new object[] //< Support inner dictionaries.. where TKey must be a string (ECMA-404).
			{
				"{\"X\":{\"A\":\"B\",\"C\":\"D\"},\"Y\":{\"E\":123,\"F\":45},\"Z\":{\"A\":[1,{}]}}",
				new Dictionary<string, object>()
				{
					{ "X", new Dictionary<string, string>() { { "A", "B" }, { "C", "D" } } },
					{ "Y", new Dictionary<string, object>() { { "E", 123 }, { "F", 45F } } },
					{ "Z", new Dictionary<string, object>() { { "A", new object[] { 1, _strObj } } } }
				},
				new[] { nameof(Newtonsoft), nameof(Jil) }
			}
		};

		#endregion

		[Test]
		public void Exception_Is_Thrown_If_StringBuilder_Is_Null_Str_Str()
		{
			Assert.That(() => Json.Serialize(_strStr, null), Throws.Exception);
		}

		[Test]
		public void Exception_Is_Thrown_If_StringBuilder_Is_Null_Str_Obj()
		{
			Assert.That(() => Json.Serialize(_strObj, null), Throws.Exception);
		}

		[Test]
		public void Serializing_A_Null_Dictionary_Returns_Null_As_String_Str_Str()
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializersStr)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					Assert.That(serialize(null), Is.EqualTo("null"), name);
				}
			});
		}

		[Test]
		public void Serializing_A_Null_Dictionary_Returns_Null_As_String_Str_Obj()
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializersObj)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					Assert.That(serialize(null), Is.EqualTo("null"), name);
				}
			});
		}

		[Test]
		public void Serializing_An_Empty_Dictionary_Returns_An_Empty_Dictionary_Representation_Str_Str()
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializersStr)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					Assert.That(serialize(_strStr), Is.EqualTo("{}"), name);
				}
			});
		}

		[Test]
		public void Serializing_An_Empty_Dictionary_Returns_An_Empty_Dictionary_Representation_Str_Obj()
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializersObj)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					Assert.That(serialize(_strObj), Is.EqualTo("{}"), name);
				}
			});
		}

		[Test]
		public void Serialize_Without_Null_Or_Empty_Works_As_Expected()
		{
			var dictionary = new Dictionary<string, string>()
			{
				{ "One", null },
				{ "Two", "ZZ" },
				{ "Three", "" },
				{ "Four", " " } //< Not excluded.
			};

			var json = Json.SerializeWithoutNullOrEmpty(dictionary);
			var expected = "{\"Two\":\"ZZ\",\"Four\":\" \"}";
			Assert.That(json, Is.EqualTo(expected));
		}

		[Test]
		[TestCaseSource(nameof(_strStrItems))]
		public void Can_Serialize_As_Expected_Str_Str(string expected, IDictionary<string, string> dictionary)
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializersStr)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					var text = serialize(dictionary);
					Assert.That(text, Is.EqualTo(expected), name);
				}
			});
		}

		[Test]
		[TestCaseSource(nameof(_strObjItems))]
		public void Can_Serialize_As_Expected_Str_Obj(string expected, IDictionary<string, object> dictionary, IEnumerable<string> excluded)
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializersObj)
				{
					var name = pair.Key;
					if (excluded.Contains(name))
						continue;

					var serialize = pair.Value;
					var text = serialize(dictionary);
					Assert.That(text, Is.EqualTo(expected), name);
				}
			});
		}
	}
}
