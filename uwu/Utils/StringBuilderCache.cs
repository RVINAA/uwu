// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  StringBuilderCache
**
** Purpose: provide a cached reusable instance of stringbuilder
**          per thread  it's an optimisation that reduces the 
**          number of instances constructed and collected.
**
**  Acquire - is used to get a string builder to use of a 
**            particular size.  It can be called any number of 
**            times, if a stringbuilder is in the cache then
**            it will be returned and the cache emptied.
**            subsequent calls will return a new stringbuilder.
**
**            A StringBuilder instance is cached in 
**            Thread Local Storage and so there is one per thread
**
**  Release - Place the specified builder in the cache if it is 
**            not too big.
**            The stringbuilder should not be used after it has 
**            been released.
**            Unbalanced Releases are perfectly acceptable.  It
**            will merely cause the runtime to create a new 
**            stringbuilder next time Acquire is called.
**
**  GetStringAndRelease
**          - ToString() the stringbuilder, Release it to the 
**            cache and return the resulting string
**
===========================================================*/

namespace System.Text
{
	// XXX: Intentionally public class..
	public static class StringBuilderCache
	{
		#region Fields

		private const int MAX_BUILDER_SIZE = 2048;

		[ThreadStatic]
		private static StringBuilder _cachedInstance;

		#endregion

		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= MAX_BUILDER_SIZE)
			{
				var cachedInstance = _cachedInstance;
				if (cachedInstance != null && capacity <= cachedInstance.Capacity)
				{
					_cachedInstance = null;
					cachedInstance.Clear();
					return cachedInstance;
				}
			}

			return new StringBuilder(capacity);
		}

		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= MAX_BUILDER_SIZE)
				_cachedInstance = sb;
		}

		public static string GetStringAndRelease(StringBuilder sb)
		{
			var result = sb.ToString();
			Release(sb);

			return result;
		}
	}
}
