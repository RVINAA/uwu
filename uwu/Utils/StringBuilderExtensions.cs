using System.Runtime.CompilerServices;

namespace System.Text
{
	internal static class StringBuilderExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AppendEscaped(this StringBuilder sb, string text)
		{
			var num = 0;
			while (num < text.Length) //< Null values already handled..
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

		public unsafe static void AppendEscapedUnsafe(this StringBuilder sb, char* pointer)
		{
			for (char* c = pointer; *c != '\0'; c++)
			{
				switch (*c)
				{
					default:
						if (*c == '"' || *c == '\\')
						{
							sb.Append('\\');
							sb.Append(*c);
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
				sb.Append(*c);
			IL_00a3:
				c++;
			}
		}
	}
}
