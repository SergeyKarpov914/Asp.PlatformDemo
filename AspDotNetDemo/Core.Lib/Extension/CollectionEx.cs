using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Clio.Demo.Extension
{
	public static class CollectionEx
	{
		public static bool IsEmpty<T>(this IEnumerable<T> list)
		{
			return list == null || !list.Any();
		}

        //public static int Count(this IEnumerable enumerable) // Count(this IEnumerable) is implemented by LINQ itself

        public static List<T> Shuffle<T>(this List<T> list)
		{
			if (null != list)
			{
				Random rnd = new Random();
				list = list.OrderBy(x => rnd.Next()).ToList();
			}
			return list;
		}

		public static IEnumerable<T> Empty<T>() => new List<T>();
	}
}
