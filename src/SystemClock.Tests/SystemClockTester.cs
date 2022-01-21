using System;
using System.Threading.Tasks;

namespace SystemClock.Tests;

public class SystemClockTester
{
    public DateTimeOffset Now()
    {
        return Clock.Now;
    }

    public async Task<DateTimeOffset> NowAsync()
    {
        return await Task.Factory.StartNew(() => Clock.Now);
    }
}