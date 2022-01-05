using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace uwu
{
	public static class Json
	{
		#region Private methods

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AppendValue(this StringBuilder sb, string text)
		{
			if (text == null)
			{
				sb.Append("\":null");
			}
			else
			{
				sb.Append("\":\"");
				sb.AppendEscaped(text);
				sb.Append('"');
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AppendEscaped(this StringBuilder sb, string text)
		{
			var num = 0;
			while (num < text.Length)
			{
				var c = text[num];
				switch (c)
				{
					default:
						if (c == '"' || c == '\\')
						{
							sb.Append('\\');
							sb.Append(c);
							goto IL_00a3;
						}
						break;
					case '\b': sb.Append("\\b"); goto IL_00a3;
					case '\t': sb.Append("\\t"); goto IL_00a3;
					case '\n': sb.Append("\\n"); goto IL_00a3;
					case '\f': sb.Append("\\f"); goto IL_00a3;
					case '\r': sb.Append("\\r"); goto IL_00a3;
					case '\v': break;
				}
				sb.Append(c);
			IL_00a3:
				num++;
			}
		}

		#endregion

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Serialize(IDictionary<string, string> dictionary, StringBuilder sb)
		{
			if (sb == null)
				throw new ArgumentNullException(nameof(StringBuilder));

			if (dictionary == null || dictionary.Count == 0)
				return;

			var enumerator = dictionary.GetEnumerator();
			enumerator.MoveNext();

			sb.Append("{\"");
			sb.AppendEscaped(enumerator.Current.Key);
			sb.AppendValue(enumerator.Current.Value);

			while (enumerator.MoveNext())
			{
				sb.Append(",\"");
				sb.AppendEscaped(enumerator.Current.Key);
				sb.AppendValue(enumerator.Current.Value);
			}

			sb.Append('}');
		}

		public static string Serialize(IDictionary<string, string> dictionary)
		{
			var sb = StringBuilderCache.Acquire(512);
			Serialize(dictionary, sb);

			return StringBuilderCache.GetStringAndRelease(sb);
		}
	}
}
