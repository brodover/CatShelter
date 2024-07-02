using System;

namespace WithAngularApp.Server.Common
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

        public static Random Get()
        {
            if (_local == null)
                Init();

            return _local;
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

        public static T NextEnum<T>(this Random random)
        where T : struct, Enum
        {
            var values = Enum.GetValues<T>();
            return values[random.Next(values.Length)];
        }

        public static T NextEnumExcludingNone<T>(this Random random)
        where T : struct, Enum
        {
            var values = Enum.GetValues<T>();
            return values[random.Next(1, values.Length)];
        }
    }
}
