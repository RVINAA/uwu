using System.Collections.Generic;

using NUnit.Framework;

namespace uwu.Tests
{
	public class JsonTests
	{
		#region Fields

		private static readonly object[] _items = new[]
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
			new object[] { "{\"A\":null,\"B\":null,\"C\":null}", new Dictionary<string, string>() { { "A", null }, { "B", null }, { "C", null } } }
		};

		#endregion

		[Test]
		[TestCaseSource(nameof(_items))]
		public void Can_Serialize_As_Expected(string expected, IDictionary<string, string> dictionary)
		{
			var text = Json.Serialize(dictionary);
			Assert.That(text, Is.EqualTo(expected));
		}
	}
}
