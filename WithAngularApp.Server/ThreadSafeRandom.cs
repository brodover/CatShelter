namespace WithAngularApp.Server
{
	public static class ThreadSafeRandom
	{
		private static readonly Random _global = new Random();
		[ThreadStatic] private static Random? _local;

		private static void Init()
		{
			int seed;
			lock (_global)
			{
				seed = _global.Next();
			}
			_local = new Random(seed);
		}

		public static int Next()
		{
			if (_local == null)
				Init();

			return _local.Next();
		}
		public static int Next(int max)
		{
			if (_local == null)
				Init();

			return _local.Next(max);
		}


		public static double NextDouble()
		{
			if (_local == null)
				Init();

			return _local.NextDouble();
		}

		public static T RandomElement<T>(this IList<T> list)
		{
			return list[Next(list.Count)];
		}

		public static T RandomElement<T>(this T[] array)
		{
			return array[Next(array.Length)];
		}
	}
}
