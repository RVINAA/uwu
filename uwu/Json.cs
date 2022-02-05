using System;
using System.Text;
using System.Collections.Generic;

namespace uwu
{
	public static class Json
	{
		public static void SerializeWithoutNullOrEmpty(IDictionary<string, string> dictionary, StringBuilder sb)
		{
			// NOTE: Perfomance depends on AVG of null or empty properties..
			//		 as has more checks but on more coincidences.. the code to execute is less.
			if (sb == null)
				throw new ArgumentNullException(nameof(StringBuilder));

			if (dictionary == null)
			{
				sb.Append("null");
				return;
			}

			var enumerator = dictionary.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;
				var value = current.Value;

				if (!string.IsNullOrEmpty(value))
				{
					sb.Append("{\"");
					sb.AppendEscaped(current.Key);
					DictionaryStringer.Write(sb, value);
					goto Loop;
				}
			}

			sb.Append("{}");
			return;

		Loop:
			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;
				var value = current.Value;

				if (!string.IsNullOrEmpty(value))
				{
					sb.Append(",\"");
					sb.AppendEscaped(current.Key);
					DictionaryStringer.Write(sb, value);
				}
			}

			sb.Append('}');
		}

		public static void Serialize(IDictionary<string, string> dictionary, StringBuilder sb)
		{
			if (sb == null)
				throw new ArgumentNullException(nameof(StringBuilder));

			if (dictionary == null)
			{
				sb.Append("null");
				return;
			}

			var enumerator = dictionary.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				sb.Append("{}");
				return;
			}

			sb.Append("{\"");
			sb.AppendEscaped(enumerator.Current.Key);
			DictionaryStringer.Write(sb, enumerator.Current.Value);

			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;

				sb.Append(",\"");
				sb.AppendEscaped(current.Key);
				DictionaryStringer.Write(sb, current.Value);
			}

			sb.Append('}');
		}

		public static void Serialize(IDictionary<string, object> dictionary, StringBuilder sb)
		{
			if (sb == null)
				throw new ArgumentNullException(nameof(StringBuilder));

			if (dictionary == null)
			{
				sb.Append("null");
				return;
			}

			var enumerator = dictionary.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				sb.Append("{}");
				return;
			}

			sb.Append("{\"");
			sb.AppendEscaped(enumerator.Current.Key);
			DictionaryStringer.Write(sb, enumerator.Current.Value);

			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;

				sb.Append(",\"");
				sb.AppendEscaped(current.Key);
				DictionaryStringer.Write(sb, current.Value);
			}

			sb.Append('}');
		}

		public static string Serialize(IDictionary<string, string> dictionary)
		{
			var sb = StringBuilderCache.Acquire(512);
			Serialize(dictionary, sb);

			return StringBuilderCache.GetStringAndRelease(sb);
		}

		public static string Serialize(IDictionary<string, object> dictionary)
		{
			var sb = StringBuilderCache.Acquire(512);
			Serialize(dictionary, sb);

			return StringBuilderCache.GetStringAndRelease(sb);
		}
	}
}
