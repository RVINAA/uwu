using System;
using System.Text;
using System.Collections.Generic;

namespace uwu
{
	public static class Json
	{
		#region Fields

		private const string NULL = "null";
		private const string EMPTY = "{}";

		#endregion

		public static string SerializeWithoutNullOrEmpty(IDictionary<string, string> dictionary)
		{
			if (dictionary == null)
				return NULL;

			StringBuilder sb;
			var enumerator = dictionary.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;
				var value = current.Value;
				if (!string.IsNullOrEmpty(value))
				{
					sb = StringBuilderCache.Acquire(512);
					sb.Append("{\"");
					sb.AppendEscaped(current.Key);
					DictionaryStringer.Write(sb, value);
					goto Loop;
				}
			}

			return EMPTY;

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

			return StringBuilderCache.GetStringAndRelease(sb);
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

		public unsafe static string SerializeUnsafe(IDictionary<string, string> dictionary)
		{
			if (dictionary == null)
				return NULL;

			var enumerator = dictionary.GetEnumerator();
			if (!enumerator.MoveNext())
				return EMPTY;

			var sb = StringBuilderCache.Acquire(512);
			sb.Append("{\"");

			fixed (char* key = enumerator.Current.Key)
				sb.AppendEscapedUnsafe(key);

			var value = enumerator.Current.Value;
			if (value == null)
			{
				sb.Append("\":null");
			}
			else
			{
				sb.Append("\":\"");
				fixed (char* val = value)
					sb.AppendEscapedUnsafe(val);
				sb.Append('"');
			}

			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;
				sb.Append(",\"");

				fixed (char* key = current.Key)
					sb.AppendEscapedUnsafe(key);

				value = current.Value;
				if (value == null)
				{
					sb.Append("\":null");
				}
				else
				{
					sb.Append("\":\"");
					fixed (char* val = value)
						sb.AppendEscapedUnsafe(val);
					sb.Append('"');
				}
			}

			sb.Append('}');

			return StringBuilderCache.GetStringAndRelease(sb);
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
