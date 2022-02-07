using System;
using System.Collections.Generic;

using Jil;
using Newtonsoft.Json;
using NUnit.Framework;

namespace uwu.Tests
{
	public class EnumerableTests
	{
		#region Fields

		// NOTE: In order to check that, by default.. I'm doing the same as other serializers.
		private static readonly IDictionary<string, Func<IEnumerable<object>, string>> _serializers = new Dictionary<string, Func<IEnumerable<object>, string>>()
		{
			{ nameof(Utf8Json), x => Utf8Json.JsonSerializer.ToJsonString(x) },
			{ nameof(Newtonsoft), x => JsonConvert.SerializeObject(x) },
			{ nameof(Jil), x => JSON.SerializeDynamic(x) },
			{ nameof(uwu), x => Json.Serialize(x) }
		};

		private static readonly object[] _items = new object[]
		{
			new object[]
			{
				"[\"Dummy\",null,3,14.09,\"\\n\\n\",\"\\\"\"]",
				new object[] { "Dummy", null, 3, 14.09F, "\n\n", '\"' }
			}
		};

		#endregion

		[Test]
		public void Serializing_A_Null_Enumerable_Returns_Null_As_String()
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializers)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					Assert.That(serialize(null), Is.EqualTo("null"), name);
				}
			});
		}

		[Test]
		public void Serializing_An_Empty_Enumerable_Returns_An_Empty_Enumerable_Representation()
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializers)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					Assert.That(serialize(Array.Empty<object>()), Is.EqualTo("[]"), name);
				}
			});
		}

		[Test]
		[TestCaseSource(nameof(_items))]
		public void Can_Serialize_As_Expected(string expected, IEnumerable<object> enumerable)
		{
			Assert.Multiple(() =>
			{
				foreach (var pair in _serializers)
				{
					var name = pair.Key;
					var serialize = pair.Value;
					var text = serialize(enumerable);
					Assert.That(text, Is.EqualTo(expected), name);
				}
			});
		}
	}
}
