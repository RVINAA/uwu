using System;
using System.Text;
using System.Collections.Generic;

namespace uwu
{
	public static class Json
	{
		#region Fields

		private const string EMPTY_DICT = "{}";
		private const string EMPTY_ENUM = "[]";
		private const string NULL = "null";
		private const int CAPACITY = 1024;

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
					sb = StringBuilderCache.Acquire(CAPACITY);
					sb.Append("{\"");
					sb.AppendEscaped(current.Key);
					DictionaryStringer.Write(sb, value);
					goto Loop;
				}
			}

			return EMPTY_DICT;

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
				return EMPTY_DICT;

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

			return sb.ToString();
		}

		public static string Serialize(IDictionary<string, string> dictionary)
		{
			var sb = StringBuilderCache.Acquire(CAPACITY);
			Serialize(dictionary, sb);

			return StringBuilderCache.GetStringAndRelease(sb);
		}

		public static string Serialize(IDictionary<string, object> dictionary)
		{
			var sb = StringBuilderCache.Acquire(CAPACITY);
			Serialize(dictionary, sb);

			return StringBuilderCache.GetStringAndRelease(sb);
		}

		public static string Serialize(IEnumerable<object> enumerable)
		{
			if (enumerable == null)
				return NULL;

			var enumerator = enumerable.GetEnumerator();
			if (!enumerator.MoveNext())
				return EMPTY_ENUM;

			var sb = StringBuilderCache.Acquire(CAPACITY);
			sb.Append('[');
			EnumerableStringer.Write(sb, enumerator.Current);

			while (enumerator.MoveNext())
			{
				sb.Append(',');
				EnumerableStringer.Write(sb, enumerator.Current);
			}

			sb.Append(']');

			return StringBuilderCache.GetStringAndRelease(sb);
		}
	}
}
