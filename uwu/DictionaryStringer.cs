﻿using System;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace uwu
{
	internal static class DictionaryStringer
	{
		#region Private methods

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, bool value)
		{
			sb.Append(value ? "\":true" : "\":false");
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, sbyte value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, byte value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, char value)
		{
			switch (value)
			{
				default:
					if (value == '"' || value == '\\')
					{
						sb.Append("\":\"\\");
						sb.Append(value);
						sb.Append('"');
						return;
					}
					break;
				case '\b': sb.Append("\":\"\\b\""); return;
				case '\t': sb.Append("\":\"\\t\""); return;
				case '\n': sb.Append("\":\"\\n\""); return;
				case '\f': sb.Append("\":\"\\f\""); return;
				case '\r': sb.Append("\":\"\\r\""); return;
				case '\v': break;
			}
			sb.Append("\":\"");
			sb.Append(value);
			sb.Append('"');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, short value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, int value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, long value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, float value)
		{
			sb.Append("\":");
			sb.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, double value)
		{
			sb.Append("\":");
			sb.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, decimal value)
		{
			sb.Append("\":");
			sb.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, ushort value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, uint value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, ulong value)
		{
			sb.Append("\":");
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, Guid value)
		{
			sb.Append("\":\"");
			sb.Append(value.ToString("D"));
			sb.Append('"');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, DateTime value)
		{
			sb.Append("\":\""); // ISO 8601
			sb.Append(value.ToString("o", CultureInfo.InvariantCulture));
			sb.Append('"');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, TimeSpan value)
		{
			sb.Append("\":\"");
			sb.Append(value.ToString());
			sb.Append('"');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, IDictionary value)
		{
			// TODO: Optimize.. I don't like this.
			var enumerator = value.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				sb.Append("\"{}");
				return;
			}

			sb.Append("\":{\"");
			sb.AppendEscaped(enumerator.Key.UnboxAsStringOrThrow());
			Write(sb, enumerator.Value);

			while (enumerator.MoveNext())
			{
				sb.Append(",\"");
				sb.AppendEscaped(enumerator.Key.UnboxAsStringOrThrow());
				Write(sb, enumerator.Value);
			}

			sb.Append('}');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, IEnumerable value)
		{
			var enumerator = value.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				sb.Append("\":[]");
				return;
			}

			sb.Append("\":[");
			EnumerableStringer.Write(sb, enumerator.Current);

			while (enumerator.MoveNext())
			{
				sb.Append(',');
				EnumerableStringer.Write(sb, enumerator.Current);
			}

			sb.Append(']');
		}

		#endregion

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write(StringBuilder sb, string value)
		{
			if (value == null)
			{
				sb.Append("\":null");
			}
			else
			{
				sb.Append("\":\"");
				sb.AppendEscaped(value);
				sb.Append('"');
			}
		}

		public static void Write(StringBuilder sb, object obj)
		{
			if (obj is string @string)
				Write(sb, @string);
			else if (obj is bool @bool)
				Write(sb, @bool);
			else if (obj is int @int)
				Write(sb, @int);
			else if (obj is uint @uint)
				Write(sb, @uint);
			else if (obj is char @char)
				Write(sb, @char);
			else if (obj is decimal @decimal)
				Write(sb, @decimal);
			else if (obj is double @double)
				Write(sb, @double);
			else if (obj is float @float)
				Write(sb, @float);
			else if (obj is long @long)
				Write(sb, @long);
			else if (obj is ulong @ulong)
				Write(sb, @ulong);
			else if (obj is short @short)
				Write(sb, @short);
			else if (obj is ushort @ushort)
				Write(sb, @ushort);
			else if (obj is sbyte @sbyte)
				Write(sb, @sbyte);
			else if (obj is byte @byte)
				Write(sb, @byte);
			else if (obj is Guid guid)
				Write(sb, guid);
			else if (obj is DateTime dateTime)
				Write(sb, dateTime);
			else if (obj is TimeSpan timeSpan)
				Write(sb, timeSpan);
			else if (obj is IDictionary dictionary) //< TODO?: Handle recursion / deep?
				Write(sb, dictionary);
			else if (obj is IEnumerable enumerable) //< TODO?: Handle recursion / deep?
				Write(sb, enumerable);
			else if (obj == null)
				sb.Append("\":null");
			else throw new NotSupportedException("Inner type not supported.");
		}
	}
}
