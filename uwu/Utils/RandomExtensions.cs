using System;
using System.Runtime.CompilerServices;

namespace uwu
{
	internal static class RandomExtensions
	{
		/// <summary>
		/// Btw, as ECMA-404 defines as invalid a JSON object with a key which is not a string (ie. a number)..
		///		instead of handle (convert or whatever) inner dictionary keys (boxed) to string.. will try to unbox as string or throw.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string UnboxAsStringOrThrow(this object obj) => obj as string ?? throw new ArgumentNullException("Not a string.");
	}
}
