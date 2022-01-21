using System;
using System.Clock;
using System.Linq;

namespace SystemClock
{
    public static class Clock
    {
        private static readonly RobustThreadLocal<Func<DateTimeOffset>> GetDateTimeOffset =
            new RobustThreadLocal<Func<DateTimeOffset>>(() => () => DateTimeOffset.Now);

        public static DateTimeOffset Today => GetDateTimeOffset.Value().Date;

        public static DateTimeOffset Now => GetDateTimeOffset.Value();

        public static DateTimeOffset UtcNow => GetDateTimeOffset.Value().ToUniversalTime();
        
        /// <summary>
        /// Sets a fixed (deterministic) time for the current thread to return by <see cref="Clock"/>.
        /// </summary>
        public static void Set(DateTime time)
        {
            if (new[] { DateTimeKind.Local, DateTimeKind.Unspecified }.All(kind => kind != time.Kind))
            {
                time = time.ToLocalTime();
            }

            GetDateTimeOffset.Value = () => time;
        }

        /// <summary>
        /// Resets <see cref="Clock"/> to return the current <see cref="System.DateTimeOffset.Now"/>.
        /// </summary>
        public static void Reset()
        {
            GetDateTimeOffset.Value = () => DateTime.Now;
        }
    }
}