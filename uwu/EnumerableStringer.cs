using System;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace uwu
{
	internal static class EnumerableStringer
	{
		#region Private methods

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, bool value)
		{
			sb.Append(value ? "true" : "false");
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, sbyte value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, byte value)
		{
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
						sb.Append("\"\\");
						sb.Append(value);
						sb.Append('"');
						return;
					}
					break;
				case '\b': sb.Append("\"\\b\""); return;
				case '\t': sb.Append("\"\\t\""); return;
				case '\n': sb.Append("\"\\n\""); return;
				case '\f': sb.Append("\"\\f\""); return;
				case '\r': sb.Append("\"\\r\""); return;
				case '\v': break;
			}
			sb.Append('"');
			sb.Append(value);
			sb.Append('"');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, short value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, int value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, long value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, float value)
		{
			sb.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, double value)
		{
			sb.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, decimal value)
		{
			sb.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, ushort value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, uint value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, ulong value)
		{
			sb.Append(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, string value)
		{
			if (value == null)
			{
				sb.Append("null");
			}
			else
			{
				sb.Append('"');
				sb.AppendEscaped(value);
				sb.Append('"');
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, DateTime value)
		{
			sb.Append('"'); // ISO 8601
			sb.Append(value.ToString("o", CultureInfo.InvariantCulture));
			sb.Append('"');
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, TimeSpan value)
		{
			sb.Append('"');
			sb.Append(value.ToString());
			sb.Append('"');
		}

#if DISABLED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, KeyValuePair<string, string> value)
		{
			sb.Append("{\"");
			sb.AppendEscaped(value.Key);

			if (value.Value == null)
			{
				sb.Append("\":null}");
			}
			else
			{
				sb.Append("\":\"");
				sb.AppendEscaped(value.Value);
				sb.Append("\"}");
			}
		}
#endif

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write(StringBuilder sb, IEnumerable value)
		{
			var enumerator = value.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				sb.Append("[]");
				return;
			}

			sb.Append('[');
			Write(sb, enumerator.Current);

			while (enumerator.MoveNext())
			{
				sb.Append(',');
				Write(sb, enumerator.Current);
			}

			sb.Append(']');
		}

#endregion

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
			else if (obj is DateTime dateTime)
				Write(sb, dateTime);
			else if (obj is TimeSpan timeSpan)
				Write(sb, timeSpan);
#if DISABLED
			else if (obj is KeyValuePair<string, string> pairStr)
				Write(sb, pairStr);
#endif
			// TODO: KeyValuePair<string, object>
			else if (obj is IEnumerable enumerable) //< TODO?: Handle recursion / deep?
				Write(sb, enumerable);
			else if (obj == null)
				sb.Append("null");
			else throw new NotSupportedException("Inner type not supported.");
		}
	}
}
