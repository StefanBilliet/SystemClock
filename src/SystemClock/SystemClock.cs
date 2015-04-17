using System;
using System.Linq;

namespace SystemClock {
  /// <summary>
  /// Provides access to system time while allowing it to be set to a fixed <see cref="DateTime"/> value.
  /// </summary>
  /// <remarks>
  /// This class is thread safe.
  /// </remarks>
  public static class SystemClock {
    private static readonly RobustThreadLocal<Func<DateTime>> GetTime = new RobustThreadLocal<Func<DateTime>>(() => () => DateTime.Now);

    /// <inheritdoc cref="DateTime.Today"/>
    public static DateTime Today {
      get { return GetTime.Value().Date; }
    }

    /// <inheritdoc cref="DateTime.Now"/>
    public static DateTime Now {
      get { return GetTime.Value(); }
    }

    /// <inheritdoc cref="DateTime.UtcNow"/>
    public static DateTime UtcNow {
      get { return GetTime.Value().ToUniversalTime(); }
    }

    /// <summary>
    /// Sets a fixed (deterministic) time for the current thread to return by <see cref="SystemClock"/>.
    /// </summary>
    public static void Set(DateTime time) {
      if (!new [] {DateTimeKind.Local, DateTimeKind.Unspecified}.Contains(time.Kind))
        time = time.ToLocalTime();

      GetTime.Value = () => time;
    }

    /// <summary>
    /// Resets <see cref="SystemClock"/> to return the current <see cref="DateTime.Now"/>.
    /// </summary>
    public static void Reset() {
      GetTime.Value = () => DateTime.Now;
    }
  }
}