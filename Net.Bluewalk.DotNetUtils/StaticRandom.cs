using System;
using System.Threading;

namespace Net.Bluewalk.DotNetUtils
{
    public static class StaticRandom
    {
        private static int _seed;

        private static readonly ThreadLocal<Random>
            ThreadLocal = new(() => new Random(Interlocked.Increment(ref _seed)));

        static StaticRandom()
        {
            _seed = Environment.TickCount;
        }

        public static Random Instance => ThreadLocal?.Value;
    }
}
