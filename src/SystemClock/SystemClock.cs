using System;
using System.Linq;
using System.Threading;

namespace SystemClock
{
    public static class Clock
    {
        private static readonly AsyncLocal<Func<DateTime>> NowProvider = new();

        /// <inheritdoc cref="System.DateTime.Today"/>
        public static DateTime Today => Now.Date;

        /// <inheritdoc cref="System.DateTime.Now"/>
        public static DateTime Now => NowProvider.Value?.Invoke() ?? DateTime.Now;

        /// <inheritdoc cref="System.DateTime.UtcNow"/>
        public static DateTime UtcNow => Now.ToUniversalTime();
        
        /// <summary>
        /// Sets a fixed (deterministic) time for the current thread to return by <see cref="Clock"/>.
        /// </summary>
        public static void Set(DateTime time)
        {
            if (new[] {DateTimeKind.Local, DateTimeKind.Unspecified}.All(kind => kind != time.Kind))
            {
                time = time.ToLocalTime();
            }

            NowProvider.Value = () => time;
        }

        /// <summary>
        /// Resets <see cref="Clock"/> to return the current <see cref="System.DateTimeOffset.Now"/>.
        /// </summary>
        public static void Reset()
        {
            NowProvider.Value = () => DateTime.Now;
        }
    }
}